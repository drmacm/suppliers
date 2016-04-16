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
    /// <summary>Contains data access methods related to <see cref="SupplierGroupEF"/>.</summary>
    public class SupplierGroupEF : ISupplierGroupDal
    {
        private readonly SuppliersContext context = new SuppliersContext();

        /// <summary>Creates a new instance of <see cref="SupplierGroupEF"/></summary>
        /// <param name="context">EntityFramework context used to manage the persistence to database.</param>
        public SupplierGroupEF(SuppliersContext context)
        {
            this.context = context;
        }

        public IList<SupplierGroup> GetAll()
        {
            var groupsEf = context.Groups;
            if (groupsEf != null && groupsEf.Any())
            {
                var groupsList = groupsEf.ToList();
                return groupsList.Select(g => g.ToSupplierGroup()).ToList();
            }
            return new List<SupplierGroup>();
        }

        public SupplierGroup GetOne(int id)
        {
            var sqlSupplierGroup = context.Groups.FirstOrDefault(s => s.Id == id);

            if (sqlSupplierGroup == default(SqlSupplierGroup)) throw new ArgumentException(string.Format("Supplier group with id {0} does not exist", id));

            return sqlSupplierGroup.ToSupplierGroup();
        }

        public void Create(SupplierGroup group)
        {
            var sqlSupplierGroup = SqlSupplierGroup.FromSupplierGroup(group);
            context.Groups.Add(sqlSupplierGroup);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var sqlSupplierGroup = context.Groups.FirstOrDefault(s => s.Id == id);
            context.Groups.Remove(sqlSupplierGroup);
            context.SaveChanges();
        }
        
        public void Update(SupplierGroup group)
        {
            var sqlSupplierGroup = SqlSupplierGroup.FromSupplierGroup(group);
            context.SetModified(sqlSupplierGroup);
            context.SaveChanges();
        }
    }
}
