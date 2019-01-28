using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Data
{
    public class ApplicationDbContextFactory : IApplicationDbContextFactory
    {
        public IApplicationDbContext Create(IDbContextOptions options = null)
        {
            return new ApplicationDbContext((DbContextOptions<ApplicationDbContext>)options ?? new DbContextOptions<ApplicationDbContext>());
        }
    }
}
