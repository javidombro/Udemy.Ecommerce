using System.Collections.Generic;
using System.Threading.Tasks;
using Udemy.Ecommerce.Domain.Entity;

namespace Udemy.Ecommerce.Infraestructure.Interface
{
    public interface ICustomerRepository
    {
        bool Insert(Customer customer);
        bool Update(Customer customer);
        bool Delete(string customerId);
        Customer Get(string customerId);
        IEnumerable<Customer> GetAll();

        #region Async
        Task<bool> InsertAsync(Customer customer);
        Task<bool> UpdateAsync(Customer customer);
        Task<bool> DeleteAsync(string customerId);
        Task<Customer> GetAsync(string customerId);
        Task<IEnumerable<Customer>> GetAllAsync();
        #endregion
    }
}
