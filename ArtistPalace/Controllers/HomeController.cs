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
                
                if (!string.IsNullOrWhiteSpace(artistsQuery?.Type))
                {
                    builder.Where("Type = @type", new {type = artistsQuery.Type});
                }

                Console.WriteLine(template.RawSql.ToString());
                var artists = connection.Query<Artist>(template.RawSql.ToString()).ToList();
                return View(artists);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}