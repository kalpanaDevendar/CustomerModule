using AutoMapper;
using CustomerModule.Interface;
using CustomerModule.Models;
using CustomerModule.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Text.Json;

namespace CustomerModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDataBaseContext _dbcontext;
        private readonly IMapper _mapper;
        ICustomersService _customersService;
        public CustomerController(CustomerDataBaseContext dbcontext,IMapper mapper, ICustomersService customersService)
        {
            _dbcontext = dbcontext;
            _customersService = customersService;
            _mapper = mapper;
        }
        [Route("GetAll")]
        [HttpGet]
        public Task<List<ResultDTO>> GetAllDetails()
        {

            var result = _customersService.GetAllCustomers();  
            return result;
        }
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(ResultDTO model)
        {
            
            var id= _customersService.AddCustomers(model);
            if (id == null)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully Inserted data", id = id });
           
        }
        [Route("GetById")]
        [HttpGet]
        public Task<ResultDTO> GetDetailByID(Guid id)
        {

            var result = _customersService.GetCustomerById(id);
            return result;
        }
        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(ResultDTO model)
        {
            
            var id = _customersService.UpdateCustomer(model);
            if (id == null)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully Upadated data", id = id });
           
        }
        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete (Guid id)
        {
               

            var isDeleted = await _customersService.DeleteCustomer(id);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found.." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted data." });

        }
    }

}
