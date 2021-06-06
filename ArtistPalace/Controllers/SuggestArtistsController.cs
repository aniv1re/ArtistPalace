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
    public class SuggestArtistsController : Controller
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly ILogger<SuggestArtistsController> _logger;

        public SuggestArtistsController(ConnectionFactory connection, ILogger<SuggestArtistsController> logger)
        {
            _connectionFactory = connection;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm] SuggestArtistsQuery suggestArtistsQuery = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Execute(
                    "exec AddToSuggestArtists @twitterTag, @type, @acceptCommissions, @pricePerHour, @isAccepted, @isRejected, @artworkLink",
                    new
                    {
                        twitterTag = suggestArtistsQuery.Tag,
                        type = suggestArtistsQuery.Type,
                        acceptCommissions = suggestArtistsQuery.AcceptCommissions == "No" ? false : true,
                        pricePerHour = suggestArtistsQuery.PricePerHour,
                        isAccepted = false,
                        isRejected = false,
                    });
            }

            return View();
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