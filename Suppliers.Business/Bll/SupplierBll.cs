using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suppliers.Business.DomainModel;
using Suppliers.Business.IDal;

namespace Suppliers.Business.Business
{
    /// <summary>Contains business rules related to <see cref="Supplier"/></summary>
    public class SupplierBll
    {
        private readonly ISupplierDal supplierDal;

        /// <summary>Creates a new instance of <see cref="SupplierBll"/>.</summary>
        /// <param name="supplierDal">Provides persistence for <see cref="Supplier"/> objects.</param>
        public SupplierBll(ISupplierDal supplierDal)
        {
            this.supplierDal = supplierDal;
        }

        public IList<Supplier> GetAllSuppliers()
        {
            return supplierDal.GetAll();
        }

        public Supplier GetSupplier(int id)
        {
            return supplierDal.GetOne(id);
        }

        public void CreateSupplier(int id, string name, string address, string emailAddress, string phoneNumber, SupplierGroup group)
        {
            var supplier = new Supplier(id, name, address, emailAddress, phoneNumber, group);

            supplierDal.Create(supplier);
        }

        public void UpdateSupplier(int id, string name, string address, string emailAddress, string phoneNumber, SupplierGroup group)
        {
            var supplier = new Supplier(id, name, address, emailAddress, phoneNumber, group);

            supplierDal.Update(supplier);
        }

        public void DeleteSupplier(int id)
        {
            supplierDal.Delete(id);
        }
    }
}
