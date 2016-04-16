using System;
using System.ComponentModel.DataAnnotations.Schema;
using Suppliers.Business.DomainModel;

namespace Suppliers.EF.DataModel
{
    /// <summary>Representation of the <see cref="Supplier"/> concept, suitable for persisting to relational database.</summary>
    [Table("Supplier")]
    public class SqlSupplier
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public int GroupId { get; set; }
        public virtual SqlSupplierGroup Group { get; set; }
        #endregion

        #region Mapping
        /// <summary>Converts a domain model object to a form that is suitable for storing in a relational database.</summary>
        public static SqlSupplier FromSupplier(Supplier supplier)
        {
            if (supplier == null) throw new ArgumentNullException("Supplier can't be null.");

            return new SqlSupplier
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Address = supplier.Address,
                EmailAddress = supplier.EmailAddress.Address,
                PhoneNumber = supplier.PhoneNumber,
                GroupId = supplier.Group.Id,
            };
        }

        /// <summary>Converts a relational database representation to a domain model object.</summary>
        public Supplier ToSupplier()
        {
            return new Supplier(Id, Name, Address, EmailAddress, PhoneNumber, new SupplierGroup(Group.Id, Group.Name));
        }
        #endregion
    }
}
