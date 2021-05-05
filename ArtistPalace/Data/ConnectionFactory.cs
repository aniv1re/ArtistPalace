using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ArtistPalace.Data
{
    public class ConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            var connectionString = _configuration["ConnectionStrings:Default"];
            
            return new SqlConnection(connectionString);
        }
    }
}