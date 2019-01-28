using app.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Data
{
    public interface IApplicationDbContext : IDisposable
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Product> Products { get; set; }
        int SaveChanges();
    }
}
