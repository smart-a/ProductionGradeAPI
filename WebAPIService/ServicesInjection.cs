using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPIData;
using WebAPIData.Interfaces;
using WebAPIService.Services;

namespace WebAPIService
{
    public static class ServicesInjection
    {
        public static void Inject(IConfiguration configuration, IServiceCollection service)
        {
            service.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            service.AddScoped<IStudent, StudentServices>();
        }
    }
}
