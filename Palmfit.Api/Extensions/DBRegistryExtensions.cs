using Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Palmfit.Data.AppDbContext;
using System.Runtime.CompilerServices;

namespace API.Extensions
{
    public static class DBRegistryExtensions
    {
        public static void AddDbContextAndConfiguration(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddDbContextPool<PalmfitDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            });

            //services.Configure<EmailSettings>(_configuration.GetSection("EmailSettings"));
        }
    }
}
