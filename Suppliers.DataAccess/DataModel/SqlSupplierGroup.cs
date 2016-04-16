using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Suppliers.Business.DomainModel;

namespace Suppliers.DataAccess
{
    /// <summary>Representation of the <see cref="SupplierGroup"/> concept, suitable for persisting to relational database.</summary>
    [Table("SupplierGroup")]
    public class SqlSupplierGroup
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SqlSupplier> Suppliers { get; set; }
        #endregion

        #region Mapping
        /// <summary>Converts a domain model object to a form that is suitable for storing in a relational database.</summary>
        public static SqlSupplierGroup FromSupplierGroup(SupplierGroup group)
        {
            if (group == null) throw new ArgumentNullException("Supplier group can't be null.");

            return new SqlSupplierGroup
            {
                Id = group.Id,
                Name = group.Name,
                Suppliers = group.Suppliers.Select(s => SqlSupplier.FromSupplier(s)).ToList()
            };
        }

        /// <summary>Converts a relational database representation to a domain model object.</summary>
        public SupplierGroup ToSupplierGroup()
        {
            var supplierGroup = new SupplierGroup(Id, Name);
            Suppliers.ToList().ForEach(s => supplierGroup.AddSupplier(s.ToSupplier()));

            return supplierGroup;
        }
        #endregion
    }
}
