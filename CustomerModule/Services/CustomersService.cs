using CustomerModule.Interface;
using CustomerModule.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace CustomerModule.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly CustomerDataBaseContext _dbcontext;
        private readonly IMapper _mapper;
        public CustomersService(CustomerDataBaseContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }
        public async Task<Guid> AddCustomers(ResultDTO model)
        {
            Customer entity = null;
            using (IDbContextTransaction transaction = _dbcontext.Database.BeginTransaction())
            {
                model.CustomerId = Guid.NewGuid();
                model.OrderId = Guid.NewGuid();
                model.OrderDate = DateTime.Now;
                entity = _mapper.Map<Customer>(model);
                Order orderEntity = null;
                orderEntity=_mapper.Map<Order>(model);
                orderEntity.CustomerId = model.CustomerId;
                await _dbcontext.Orders.AddAsync(orderEntity);
                await _dbcontext.AddAsync(entity);
                await _dbcontext.SaveChangesAsync();

                transaction.Commit();
            }
            return entity.CustomerId;
        }

        public async Task<bool> DeleteCustomer(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbcontext.Database.BeginTransaction())
            {
                var data = _dbcontext.Customers.Where(x => x.CustomerId == id).FirstOrDefault();
                if (data != null)
                {

                    //delete child table data
                    if (data.Orders.Count() > 0)
                    {
                        var order = _dbcontext.Orders.Where(x => x.CustomerId == id).FirstOrDefault();
                        _dbcontext.Orders.Remove(order);
                        _dbcontext.SaveChangesAsync();
                    }
                   
                    _dbcontext.Customers.Remove(data);
                    _dbcontext.SaveChangesAsync();
                    isSuccess = true;
                }
                transaction.Commit();
            }
                
            return isSuccess;
        }

        public async Task<List<ResultDTO>> GetAllCustomers()
        {
            List<ResultDTO> list = null;
            list = await(from c in _dbcontext.Customers
                               join o in _dbcontext.Orders on c.CustomerId equals o.CustomerId
                               select new ResultDTO
                               {
                                   CustomerId = c.CustomerId,
                                   CustomerName = c.CustomerName,
                                   OrderId = o.OrderId,
                                   OrderDate = o.OrderDate,
                                   EmailId = c.EmailId,
                                   Address = c.Address
                               }).ToListAsync();
           
          var result = _mapper.Map<List<ResultDTO>>(list);
          
            return result;
        }

        public async Task<ResultDTO> GetCustomerById(Guid id)
        {
            
            ResultDTO data = await (from c in _dbcontext.Customers
                          join o in _dbcontext.Orders on c.CustomerId equals o.CustomerId
                          where c.CustomerId == id
                          select new ResultDTO
                          {
                              CustomerId = c.CustomerId,
                              CustomerName = c.CustomerName,
                              OrderId = o.OrderId,
                              OrderDate = o.OrderDate,
                              EmailId = c.EmailId,
                              Address = c.Address
                          }).FirstOrDefaultAsync();

            var result = _mapper.Map<ResultDTO>(data);

            return result;
        }

        public async Task<Guid?> UpdateCustomer(ResultDTO model)
        {
            Guid? id = model.CustomerId;

            using (IDbContextTransaction transaction = _dbcontext.Database.BeginTransaction())
            {
                var originalEntity = await _dbcontext.Customers.Where(x=>x.CustomerId==model.CustomerId).FirstOrDefaultAsync();
                if (originalEntity != null)
                {
                    _dbcontext.Entry(originalEntity).CurrentValues.SetValues(model);
                    await _dbcontext.SaveChangesAsync();
                    id = originalEntity.CustomerId;
                }
                transaction.Commit();
            }
            return id;
        }
    }
}
