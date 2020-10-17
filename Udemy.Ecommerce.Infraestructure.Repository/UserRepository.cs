using Dapper;
using Udemy.Ecommerce.Domain.Entity;
using Udemy.Ecommerce.Infraestructure.Interface;
using Udemy.Ecommerce.Transversal.Common;

namespace Udemy.Ecommerce.Infraestructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly string getUserByIdAndPassword = "UsersGetByUserAndPassword";

        public UserRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public User Authenticate(string username, string password)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = getUserByIdAndPassword;
                var parameters = new DynamicParameters();
                parameters.Add("UserName", username);
                parameters.Add("Password", password);

                var user = connection.QuerySingle<User>(query, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return user;
            }
        }
    }
}