using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suppliers.Business.DomainModel;

namespace Suppliers.Business.IDal
{
    /// <summary>Defines operations needed for persisting the <see cref="SupplierGroup"/> objects.</summary>
    public interface ISupplierGroupDal
    {
        /// <summary>Retrieves all groups.</summary>
        IList<SupplierGroup> GetAll();

        /// <summary>Retrieves a single group.</summary>
        SupplierGroup GetOne(int id);

        /// <summary>Creates a new group.</summary>
        void Create(SupplierGroup group);

        /// <summary>Updates an existing group.</summary>
        void Update(SupplierGroup group);

        /// <summary>Deletes a group.</summary>
        void Delete(int id);
    }
}
