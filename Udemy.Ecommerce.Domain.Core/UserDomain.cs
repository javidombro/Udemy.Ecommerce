using Udemy.Ecommerce.Domain.Entity;
using Udemy.Ecommerce.Domain.Interface;
using Udemy.Ecommerce.Infraestructure.Interface;

namespace Udemy.Ecommerce.Domain.Core
{
    public class UserDomain : IUserDomain
    {
        private readonly IUserRepository _userRepository;

        public UserDomain(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Authenticate(string username, string password)
        {
            return _userRepository.Authenticate(username, password);
        }
    }
}