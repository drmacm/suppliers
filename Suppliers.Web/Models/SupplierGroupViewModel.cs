using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Suppliers.Business.DomainModel;

namespace Suppliers.Web.Models
{
    public class SupplierGroupViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static SupplierGroupViewModel FromSupplierGroup(SupplierGroup supplierGroup)
        {
            return new SupplierGroupViewModel
            {
                Id = supplierGroup.Id,
                Name = supplierGroup.Name
            };
        }
    }
}