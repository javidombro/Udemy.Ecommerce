using Udemy.Ecommerce.Application.DTO;
using Udemy.Ecommerce.Transversal.Common;

namespace Udemy.Ecommerce.Application.Interface
{
    public interface IUserApplication
    {
        Response<UserDTO> Authenticate(string username, string password);
    }
}