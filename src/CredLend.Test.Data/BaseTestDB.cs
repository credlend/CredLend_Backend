using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredLend.Test.Data
{
    public abstract class BaseTestDB
    {
        public BaseTestDB()
        {

        }
    }


    public class DbTest : IDisposable
    {
        private readonly string dataBaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
        public ServiceProvider ServiceProvider { get; set; }

        public DbTest()
        {
            var ServiceCollection = new ServiceCollection();

            ServiceCollection.AddDbContext<ApplicationDataContext>(options =>
                options.UseSqlite($"Data Source ={dataBaseName}.db"), ServiceLifetime.Transient);

            ServiceProvider = ServiceCollection.BuildServiceProvider();
            using (var context = ServiceProvider.GetService<ApplicationDataContext>()) 
            {
                context.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<ApplicationDataContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
