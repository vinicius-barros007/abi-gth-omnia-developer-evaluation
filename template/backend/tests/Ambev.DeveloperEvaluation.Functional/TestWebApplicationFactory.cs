using Ambev.DeveloperEvaluation.ORM;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.Functional
{
    public class TestWebApplicationFactory : WebApplicationFactory<WebApi.Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the existing database context configuration
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DefaultContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add a new in-memory database for testing
                services.AddDbContext<DefaultContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDatabase");
                });

                // Apply migrations and seed data
                using var scope = services.BuildServiceProvider().CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<DefaultContext>();
                db.Database.EnsureCreated();
            });
        }
    }
}
