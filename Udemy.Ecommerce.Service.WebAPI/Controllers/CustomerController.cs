using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Udemy.Ecommerce.Application.DTO;
using Udemy.Ecommerce.Application.Interface;

namespace Udemy.Ecommerce.Service.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : Controller
    {

        private readonly ICustomerApplication _customerApplication;

        public CustomerController(ICustomerApplication customerApplication)
        {
            this._customerApplication = customerApplication;
        }

        #region Metodos Sincronicos

        [HttpPost("Insert")]
        public IActionResult Insert([FromBody] CustomerDTO customerDTO)
        {
            if (customerDTO == null)
            {
                return BadRequest();
            }
            var response = _customerApplication.Insert(customerDTO);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] CustomerDTO customerDTO)
        {
            if (customerDTO == null)
            {
                return BadRequest();
            }
            var response = _customerApplication.Update(customerDTO);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpDelete("Delete/{customerId}")]
        public IActionResult Delete(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                return BadRequest();
            }
            var response = _customerApplication.Delete(customerId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpGet("Get/{customerId}")]
        public IActionResult Get(string customerId)
        {
            if (customerId == null)
            {
                return BadRequest();
            }
            var response = _customerApplication.Get(customerId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var response = _customerApplication.GetAll();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        #endregion

        #region Metodos Async

        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync([FromBody] CustomerDTO customerDTO)
        {
            if (customerDTO == null)
            {
                return BadRequest();
            }
            var response = await _customerApplication.InsertAsync(customerDTO);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync([FromBody] CustomerDTO customerDTO)
        {
            if (customerDTO == null)
            {
                return BadRequest();
            }
            var response = await _customerApplication.UpdateAsync(customerDTO);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpDelete("DeleteAsync/{customerId}")]
        public async Task<IActionResult> DeleteAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                return BadRequest();
            }
            var response = await _customerApplication.DeleteAsync(customerId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpGet("GetAsync/{customerId}")]
        public async Task<IActionResult> GetAsync(string customerId)
        {
            if (customerId == null)
            {
                return BadRequest();
            }
            var response = await _customerApplication.GetAsync(customerId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _customerApplication.GetAllAsync();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }
        #endregion
    }
}