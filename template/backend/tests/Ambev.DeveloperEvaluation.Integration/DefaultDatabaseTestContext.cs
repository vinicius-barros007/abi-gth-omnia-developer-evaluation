using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Ambev.DeveloperEvaluation.ORM;

namespace Ambev.DeveloperEvaluation.Integration;

public class DefaultDatabaseTestContext : DefaultContext 
{
    public DefaultDatabaseTestContext() : base(
        new DbContextOptionsBuilder<DefaultContext>()
        .UseInMemoryDatabase(nameof(DefaultDatabaseTestContext))
        .Options)
    {
    }

    public void CreateDatabase()
    {
        Database.EnsureCreated();
    }

    public void ResetTracking()
    {
        ChangeTracker.Clear();
    }

    public void DisposeDatabase()
    {
        Database.EnsureDeleted();
    }
}
