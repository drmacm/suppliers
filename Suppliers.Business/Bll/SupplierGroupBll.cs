using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suppliers.Business.DomainModel;
using Suppliers.Business.IDal;

namespace Suppliers.Business.Business
{
    /// <summary>Contains business rules related to <see cref="SupplierGroup"/></summary>
    public class SupplierGroupBll
    {
        private readonly ISupplierGroupDal supplierGroupDal;

        /// <summary>Creates a new instance of <see cref="SupplierGroupBll"/>.</summary>
        /// <param name="supplierGroupDal">Provides persistence for <see cref="SupplierGroup"/> objects.</param>
        public SupplierGroupBll(ISupplierGroupDal supplierGroupDal)
        {
            this.supplierGroupDal = supplierGroupDal;
        }

        public IList<SupplierGroup> GetAllSupplierGroups()
        {
            return supplierGroupDal.GetAll();
        }

        public SupplierGroup GetSupplierGroup(int id)
        {
            return supplierGroupDal.GetOne(id);
        }

        public void CreateSupplierGroup(int id, string name)
        {
            var supplierGroup = new SupplierGroup(id, name);

            supplierGroupDal.Create(supplierGroup);
        }

        public void UpdateSupplierGroup(int id, string name)
        {
            var supplierGroup = new SupplierGroup(id, name);

            supplierGroupDal.Update(supplierGroup);
        }

        public void DeleteSupplierGroup(int id)
        {
            supplierGroupDal.Delete(id);
        }
    }
}
