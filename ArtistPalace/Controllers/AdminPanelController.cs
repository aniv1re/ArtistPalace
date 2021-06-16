using System;
using System.Diagnostics;
using System.Linq;
using ArtistPalace.Data;
using Microsoft.AspNetCore.Mvc;
using ArtistPalace.Models;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using ArtistPalace.TwitterApi;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ArtistPalace.ViewModels;

namespace ArtistPalace.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly ILogger<AdminPanelController> _logger;

        public AdminPanelController(ConnectionFactory connection, ILogger<AdminPanelController> logger)
        {
            _connectionFactory = connection;
            _logger = logger;
        }

        public IActionResult Reject(string _tag)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Execute("exec UpdateRejectedSuggestArtist @twitterTag", new { twitterTag = _tag });
            }
            return Redirect("Index");
        }

        public IActionResult Accept(string _tag, string _type, string _acceptCommissions, string _pricePerHour, string _lang)
        {
            var user = TwitterHandler.GetInfo(_tag).Result;

            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Execute(
                    "exec AddToArtists @twitterLink, @nickname, @followersCount, @lang, @rank, @twitterTag, @type, @acceptCommissions, @pricePerHour",
                    new
                    {
                        twitterLink = "https://twitter.com/" + user.TwitterTag,
                        nickname = user.Nickname,
                        followersCount = user.FollowersCount,
                        lang = _lang,
                        rank = GetRank(Convert.ToInt32(user.FollowersCount)),
                        twitterTag = _tag,
                        type = _type,
                        acceptCommissions = _acceptCommissions == "false" ? false : true,
                        pricePerHour = _pricePerHour,
                    });
                connection.Execute("exec UpdateAcceptedSuggestArtist @twitterTag", new { twitterTag = _tag });
            }


            return Redirect("Index");
        }

        [Authorize]
        public IActionResult Index()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var suggestArtists = connection.Query<SuggestArtists>("select * from SuggestArtists order by IsAccepted asc").ToList();

                return View(suggestArtists);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // auth
        // used only form moderation
        public IActionResult Register()
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
        }
        
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

            return Redirect("Index");
        }
        
        private string HashPassword(string password)
        {
            var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Encoding.UTF8.GetString(bytes);
        }

        private string GetRank(int followersCount)
        {
            if (followersCount > 74999)
            {
                return "S";
            }
            else if (followersCount > 24999)
            {
                return "A";
            }
            else if (followersCount > 1000)
            {
                return "B";
            }
            else if (followersCount < 1000)
            {
                return "C";
            }

            return null;
        }
    }
}