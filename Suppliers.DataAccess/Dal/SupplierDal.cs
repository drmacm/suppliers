using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Suppliers.Business.DomainModel;
using Suppliers.Business.IDal;

namespace Suppliers.DataAccess
{
    /// <summary>Contains data access methods related to <see cref="sqlSupplier"/>.</summary>
    public class SupplierDal : ISupplierDal
    {
        private readonly SuppliersContext context;

        /// <summary>Creates a new instance of <see cref="SupplierDal"/></summary>
        /// <param name="context">EntityFramework context used to manage the persistence to database.</param>
        public SupplierDal(SuppliersContext context)
        {
            this.context = context;
        }

        public IList<Supplier> GetAll()
        {
            var suppliersDal = context.Suppliers.Include(s => s.Group);
            if (suppliersDal != null && suppliersDal.Any())
            {
                var suppliersList = suppliersDal.ToList();
                return suppliersList.Select(s => s.ToSupplier()).ToList();
            }
            return new List<Supplier>();
        }

        public Supplier GetOne(int id)
        {
            var sqlSupplier = context.Suppliers.Include(s => s.Group).FirstOrDefault(s => s.Id == id);

            if (sqlSupplier == default(SqlSupplier)) throw new ArgumentException(string.Format("Supplier with id {0} does not exist", id));

            return sqlSupplier.ToSupplier();
        }

        public void Create(Supplier supplier)
        {
            var sqlSupplier = SqlSupplier.FromSupplier(supplier);
            context.Suppliers.Add(sqlSupplier);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var sqlSupplier = context.Suppliers.Include(s => s.Group).FirstOrDefault(s => s.Id == id);
            context.Suppliers.Remove(sqlSupplier);
            context.SaveChanges();
        }
        
        public void Update(Supplier supplier)
        {
            var sqlSupplier = SqlSupplier.FromSupplier(supplier);
            context.SetModified(sqlSupplier);
            context.SaveChanges();
        }
    }
}
