using CustomerModule.Models;

namespace CustomerModule.Interface
{
    public interface ICustomersService
    {
        Task<Guid> AddCustomers(ResultDTO model);
        Task<Guid?> UpdateCustomer(ResultDTO model);
        Task<bool> DeleteCustomer(Guid id);
        Task<List<ResultDTO>> GetAllCustomers();
        Task<ResultDTO> GetCustomerById(Guid id);
    }
}
