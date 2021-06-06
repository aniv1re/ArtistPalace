using System;
using System.Diagnostics;
using System.Linq;
using ArtistPalace.Data;
using Microsoft.AspNetCore.Mvc;
using ArtistPalace.Models;
using Dapper;
using Microsoft.Extensions.Logging;

namespace ArtistPalace.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly ILogger<ArtistsController> _logger;

        public ArtistsController(ConnectionFactory connection, ILogger<ArtistsController> logger)
        {
            _connectionFactory = connection;
            _logger = logger;
        }

        public IActionResult Index([FromQuery] ArtistsQuery artistsQuery = null)
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

                var artists = connection.Query<Artist>(template.RawSql.ToString() + "order by FollowersCount desc").ToList();

                return View(artists);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
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