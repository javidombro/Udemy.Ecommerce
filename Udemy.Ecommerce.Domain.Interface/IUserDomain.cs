using Udemy.Ecommerce.Domain.Entity;

namespace Udemy.Ecommerce.Domain.Interface
{
    public interface IUserDomain
    {
        User Authenticate(string username, string password);
    }
}