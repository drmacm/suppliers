using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suppliers.Business.DomainModel;

namespace Suppliers.Business.IDal
{
    /// <summary>Defines operations needed for persisting the <see cref="Supplier"/> objects.</summary>
    public interface ISupplierDal
    {
        /// <summary>Retrieves all suppliers.</summary>
        IList<Supplier> GetAll();

        /// <summary>Retrieves a single supplier.</summary>
        Supplier GetOne(int id);

        /// <summary>Creates a new supplier.</summary>
        void Create(Supplier supplier);

        /// <summary>Updates an existing supplier.</summary>
        void Update(Supplier supplier);

        /// <summary>Deletes a supplier.</summary>
        void Delete(int id);
    }
}
