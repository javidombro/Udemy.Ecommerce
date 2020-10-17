using System;
using System.Collections.Generic;
using System.Text;
using Udemy.Ecommerce.Domain.Entity;

namespace Udemy.Ecommerce.Infraestructure.Interface
{
    public interface IUserRepository
    {
        User Authenticate(string username, string password);
    }
}
