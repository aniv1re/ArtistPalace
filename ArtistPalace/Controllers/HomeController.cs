using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ArtistPalace.Data;
using Microsoft.AspNetCore.Mvc;
using ArtistPalace.Models;
using Dapper;

namespace ArtistPalace.Controllers
{
    public class HomeController : Controller
    {
        private readonly ConnectionFactory _connection;

        public HomeController(ConnectionFactory connection)
        {
            _connection = connection;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult Artists([FromQuery] ArtistsQuery artistsQuery = null)
        {
            using (var connection = _connection.CreateConnection())
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

                Console.WriteLine(template.RawSql.ToString());
                var artists = connection.Query<Artist>(template.RawSql.ToString()).ToList();
                return View(artists);
            }
        }
        
        public IActionResult AdminPanel()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}