using CustomerModule.Interface;
using CustomerModule.Services;
using System.ComponentModel.Design;

namespace CustomerModule.Extensions
{
    public class ServiceExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ICustomersService, CustomersService>();
        }
    }
}
