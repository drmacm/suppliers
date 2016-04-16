using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace Suppliers.DataAccess.Tests.Dal
{
    /// <summary>Used to mock out the DbSet classes, to enable unit testing od data acess layer.</summary>
    public class FakeDbSet<T> : Mock<DbSet<T>> where T : class
    {
        public FakeDbSet(IEnumerable<T> data)
        {
            var mockDataQueryable = data.AsQueryable();

            As<IQueryable<T>>().Setup(x => x.Provider).Returns(mockDataQueryable.Provider);
            As<IQueryable<T>>().Setup(x => x.Expression).Returns(mockDataQueryable.Expression);
            As<IQueryable<T>>().Setup(x => x.ElementType).Returns(mockDataQueryable.ElementType);
            As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(mockDataQueryable.GetEnumerator());

            Setup(x => x.Include(It.IsAny<string>())).Returns(Object);
        }
    }
}
