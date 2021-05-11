using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ArtistPalace.Data;
using Microsoft.AspNetCore.Mvc;
using ArtistPalace.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ArtistPalace.TwitterApi;
using ArtistPalace.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ArtistPalace.Controllers
{
    public class HomeController : Controller
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ConnectionFactory connection, ILogger<HomeController> logger)
        {
            _connectionFactory = connection;
            _logger = logger;
        }

        public IActionResult Index()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var suggestArtists = connection.Query<SuggestArtists>("select top 5 * from SuggestArtists order by Id desc ").ToList();
                return View(suggestArtists);
            }
        }

        [HttpGet]
        public IActionResult SuggestArtists()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SuggestArtists([FromForm] SuggestArtistsQuery suggestArtistsQuery = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Execute(
                    "exec AddToSuggestArtists @twitterTag, @type, @acceptCommissions, @pricePerHour, @isAccepted",
                    new
                    {
                        twitterTag = suggestArtistsQuery.Tag,
                        type = suggestArtistsQuery.Type,
                        acceptCommissions = suggestArtistsQuery.AcceptCommissions == "No" ? false : true,
                        pricePerHour = suggestArtistsQuery.PricePerHour,
                        isAccepted = false
                    });
            }

            return View();
        }

        public IActionResult Artists([FromQuery] ArtistsQuery artistsQuery = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var builder = new SqlBuilder();
                var template = builder.AddTemplate("select * from artists /**where**/");

                if (!string.IsNullOrWhiteSpace(artistsQuery?.NickName))
                {
                    builder.Where($"Nickname Like '%{artistsQuery.NickName}%'");
                }

                if (!string.IsNullOrWhiteSpace(artistsQuery?.Country) && artistsQuery?.Country != "All")
                {
                    builder.Where($"Country = '{artistsQuery.Country}'");
                }

                if (!string.IsNullOrWhiteSpace(artistsQuery?.Type) && artistsQuery?.Type != "All")
                {
                    builder.Where($"Type = '{artistsQuery.Type}'");
                }

                if (!string.IsNullOrWhiteSpace(artistsQuery?.Rank) && artistsQuery?.Rank != "All")
                {
                    builder.Where($"Rank = '{artistsQuery.Rank}'");
                }

                if (!string.IsNullOrWhiteSpace(artistsQuery?.Commissions) && artistsQuery?.Commissions != "All")
                {
                    builder.Where($"AcceptCommissions = '{(artistsQuery.Commissions == "Yes" ? true : false)}'");
                }

                _logger.Log(LogLevel.Information, template.RawSql.ToString());

                var artists = connection.Query<Artist>(template.RawSql.ToString() + "order by FollowersCount desc").ToList();

                return View(artists);
            }
        }

        [Authorize]
        public IActionResult AdminPanel()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var suggestArtists = connection.Query<SuggestArtists>("select * from SuggestArtists").ToList();
                return View(suggestArtists);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        
        // auth
        
        /*public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Execute("insert into Users(Email, PasswordHash) values (@email, @hash)",
                    new
                    {
                        email = viewModel.Email,
                        hash = HashPassword(viewModel.Password)
                    });
            }

            return View();
        }*/
        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var user = connection.QuerySingleOrDefault<User>(@"select * from Users where Email = @email", new
                {
                    email = viewModel.Email
                });

                if (user == null)
                {
                    return View();
                }

                if (HashPassword(viewModel.Password) != user.PasswordHash)
                {
                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
                };

                var identity = new ClaimsIdentity(claims, "ApplicationCookie",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));
            }

            return View("Index");
        }
        
        private string HashPassword(string password)
        {
            var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Encoding.UTF8.GetString(bytes);
        }
    }
}