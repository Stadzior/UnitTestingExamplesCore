using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace app.Data
{
    public interface IApplicationDbContextFactory
    {
        IApplicationDbContext Create(IDbContextOptions options = null);
    }
}
