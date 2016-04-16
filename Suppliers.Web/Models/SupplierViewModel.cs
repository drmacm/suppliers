using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Suppliers.Business.DomainModel;

namespace Suppliers.Web.Models
{
    public class SupplierViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Invalid phone number - must be in 9-digit format (xxxxxxxxx)")]
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