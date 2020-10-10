using System.Collections.Generic;
using System.Threading.Tasks;
using Udemy.Ecommerce.Domain.Entity;
using Udemy.Ecommerce.Domain.Interface;
using Udemy.Ecommerce.Infraestructure.Interface;

namespace Udemy.Ecommerce.Domain.Core
{
    public class CustomerDomain : ICustomerDomain
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerDomain(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        //EN UN CASO REAL LOS METODOS NO SERIAN DIRECTOS SINO QUE TENDRIAN LOGICA DEL NEGOCIO.

        public bool Delete(string customerId)
        {
            return customerRepository.Delete(customerId);
        }

        public Task<bool> DeleteAsync(string customerId)
        {
            return customerRepository.DeleteAsync(customerId);
        }

        public Customer Get(string customerId)
        {
            return customerRepository.Get(customerId);
        }

        public IEnumerable<Customer> GetAll()
        {
            return customerRepository.GetAll();
        }

        public Task<IEnumerable<Customer>> GetAllAsync()
        {
            return customerRepository.GetAllAsync();
        }

        public Task<Customer> GetAsync(string customerId)
        {
            return customerRepository.GetAsync(customerId);
        }

        public bool Insert(Customer customer)
        {
            return customerRepository.Insert(customer);
        }

        public Task<bool> InsertAsync(Customer customer)
        {
            return customerRepository.InsertAsync(customer);
        }

        public bool Update(Customer customer)
        {
            return customerRepository.Update(customer);
        }

        public Task<bool> UpdateAsync(Customer customer)
        {
            return customerRepository.UpdateAsync(customer);
        }
    }
}
