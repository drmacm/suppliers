using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Suppliers.Business.DomainModel;
using Suppliers.Business.IDal;
using Suppliers.EF.DataModel;

namespace Suppliers.EF.Dal
{
    /// <summary>Contains data access methods related to <see cref="sqlSupplier"/>.</summary>
    public class SupplierEF : ISupplierDal
    {
        private readonly SuppliersContext context;

        /// <summary>Creates a new instance of <see cref="SupplierEF"/></summary>
        /// <param name="context">EntityFramework context used to manage the persistence to database.</param>
        public SupplierEF(SuppliersContext context)
        {
            this.context = context;
        }

        public IList<Supplier> GetAll()
        {
            var suppliersEf = context.Suppliers.Include(s => s.Group);
            if (suppliersEf != null && suppliersEf.Any())
            {
                var suppliersList = suppliersEf.ToList();
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
