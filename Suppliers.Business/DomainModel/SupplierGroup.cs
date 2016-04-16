using System;
using System.Collections.Generic;
using System.Linq;

namespace Suppliers.Business.DomainModel
{
    /// <summary>Represents a named collection of suppliers.</summary>
    public class SupplierGroup
    {
        private int id;
        /// <summary>Uniquely identifies the supplier group.</summary>
        public int Id
        {
            get { return id; }
            set
            {
                if (id != 0 && id != value) throw new InvalidOperationException("Supplier group id can't change after setting it");
                {
                    id = value;
                }
            }

        }

        /// <summary>A name of the group.</summary>
        public string Name { get; private set; }

        private IList<Supplier> suppliers;
        /// <summary>A collection of suppliers that belong to this group.</summary>
        public IList<Supplier> Suppliers { get { return suppliers; } }

        /// <summary>Creates a new instance of <see cref="SupplierGroup"/>.</summary>
        /// <exception cref="ArgumentException">Thrown when group name is not provided.</exception>
        public SupplierGroup(int id, string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Supplier group's name must be provided.");

            Id = id;
            Name = name;
            suppliers = new List<Supplier>();
        }

        /// <summary>Adds a new supplier to the collection.</summary>
        /// <param name="supplier">The supplier we are adding.</param>
        /// <returns>True, if supplier was successfully added to collection, otherwise false.</returns>
        public bool AddSupplier(Supplier supplier)
        {
            if (supplier != null && !suppliers.Any(s => s.EmailAddress.Address == supplier.EmailAddress.Address))
            {
                suppliers.Add(supplier);
                if (supplier.Group != null && supplier.Group != this)
                {
                    supplier.UpdateGroup(this);
                }
               
                return true;
            }
            return false;
        }
    }
}
