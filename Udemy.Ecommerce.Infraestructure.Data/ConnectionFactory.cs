using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Udemy.Ecommerce.Transversal.Common;

namespace Udemy.Ecommerce.Infraestructure.Data
{
    public class ConnectionFactory : IConnectionFactory
    {

        private readonly IConfiguration _configuration;
        private readonly string connectionString = "";

        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetConnection
        {
            get
            {
                var sqlConnection = new SqlConnection();
                if (sqlConnection == null) return null;
                sqlConnection.ConnectionString = _configuration.GetConnectionString(connectionString);
                sqlConnection.Open();
                return sqlConnection;
            }
        }
    }
}