using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Suppliers.Business.DomainModel;

namespace Suppliers.Web.Models
{
    public class SupplierViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public int GroupId { get; set; }

        public static SupplierViewModel FromSupplier(Supplier supplier)
        {
            return new SupplierViewModel
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Address = supplier.Address,
                EmailAddress = supplier.EmailAddress.Address,
                PhoneNumber = supplier.PhoneNumber,
                GroupId = supplier.Group.Id
            };
        }
    }
}