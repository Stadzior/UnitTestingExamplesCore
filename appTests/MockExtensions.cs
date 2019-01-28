using app.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace appTests
{
    public static class MockExtensions
    {
        public static void SetupEntityCollection<T>(this Mock<IApplicationDbContext> context, Expression<Func<IApplicationDbContext, DbSet<T>>> dbSetExpression, IEnumerable<T> collection) where T : class
        {
            var queryable = collection.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            context.Setup(dbSetExpression).Returns(dbSetMock.Object);
        }
    }
}
