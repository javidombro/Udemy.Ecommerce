using System.Collections.Generic;
using System.Threading.Tasks;
using Udemy.Ecommerce.Application.DTO;
using Udemy.Ecommerce.Transversal.Common;

namespace Udemy.Ecommerce.Application.Interface
{
    public interface ICustomerApplication
    {
        Response<bool> Insert(CustomerDTO customerDTO);
        Response<bool> Update(CustomerDTO customerDTO);
        Response<bool> Delete(string customerId);
        Response<CustomerDTO> Get(string customerId);
        Response<IEnumerable<CustomerDTO>> GetAll();

        #region Async
        Task<Response<bool>> InsertAsync(CustomerDTO customerDTO);
        Task<Response<bool>> UpdateAsync(CustomerDTO customerDTO);
        Task<Response<bool>> DeleteAsync(string customerId);
        Task<Response<CustomerDTO>> GetAsync(string customerId);
        Task<Response<IEnumerable<CustomerDTO>>> GetAllAsync();
        #endregion
    }
}
