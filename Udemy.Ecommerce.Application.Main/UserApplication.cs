using AutoMapper;
using System;
using Udemy.Ecommerce.Application.DTO;
using Udemy.Ecommerce.Application.Interface;
using Udemy.Ecommerce.Domain.Interface;
using Udemy.Ecommerce.Transversal.Common;

namespace Udemy.Ecommerce.Application.Main
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserDomain _userDomain;
        private readonly IMapper _mapper;

        public UserApplication(IUserDomain userDomain, IMapper mapper)
        {
            _mapper = mapper;
            _userDomain = userDomain;
        }

        public Response<UserDTO> Authenticate(string username, string password)
        {
            var response = new Response<UserDTO>();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                response.Message = "Please insert all params";
                return response;
            }
            try
            {
                var user = _userDomain.Authenticate(username, password);
                response.Data = _mapper.Map<UserDTO>(user);
                response.IsSuccess = true;
                response.Message = "Authenticated";
            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = true;
                response.Message = "User does not exist";
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }
    }
}