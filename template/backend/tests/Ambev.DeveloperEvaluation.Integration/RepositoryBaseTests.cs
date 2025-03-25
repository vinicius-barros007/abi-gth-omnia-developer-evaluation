using Ambev.DeveloperEvaluation.ORM;

namespace Ambev.DeveloperEvaluation.Integration;

public class RepositoryBaseTests : IDisposable
{
    protected readonly DefaultDatabaseTestContext _context;
    protected readonly UnitOfWork _unitOfWork;

    public RepositoryBaseTests()
    {
        _context = new DefaultDatabaseTestContext();
        _context.CreateDatabase();

        _unitOfWork = new UnitOfWork(_context); 
    }

    public void Dispose()
    {
        _context.DisposeDatabase();
    }
}
