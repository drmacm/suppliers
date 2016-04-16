using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Suppliers.Business.DomainModel
{
    /// <summary>Stores information about a single supplier.</summary>
    public class Supplier
    {
        private readonly Regex phoneNumberRegex = new Regex(@"^\d{9}$", RegexOptions.Singleline);

        private int id;
        /// <summary>Uniquely identifies the supplier.</summary>
        public int Id
        {
            get { return id; }
            set
            {
                if (id != 0 && id != value) throw new InvalidOperationException("Supplier id can't change after initial setting.");
                {
                    id = value;
                }
            }

        }

        /// <summary>A name of supplier.</summary>
        public string Name { get; private set; }

        /// <summary>An address of supplier.</summary>
        public string Address { get; private set; }

        /// <summary>An email address of supplier.</summary>
        public MailAddress EmailAddress { get; private set; }

        /// <summary>A phone number of supplier</summary>
        public string PhoneNumber { get; private set; }

        /// <summary>The <see cref="SupplierGroup"/> that this supplier belongs to.</summary>
        public SupplierGroup Group { get; private set; }

        /// <summary>Creates a new instance of <see cref="Supplier"/>.</summary>
        /// <exception cref="ArgumentException">Thrown when any of the string arguments is invalid (null or empty).</exception>
        /// <exception cref="ArgumentNullException">Thrown when supplier group is not provided.</exception>
        /// <exception cref="FormatException">Thrown when either email or phone number have incorrect format.</exception>
        public Supplier(int id, string name, string address, string emailAddress, string phoneNumber, SupplierGroup group)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Supplier's name must be provided.");
            if (string.IsNullOrEmpty(address)) throw new ArgumentException("Supplier's address must be provided.");
            if (string.IsNullOrEmpty(emailAddress)) throw new ArgumentException("Supplier's email address must be provided.");
            if (string.IsNullOrEmpty(phoneNumber)) throw new ArgumentException("Supplier's phone number must be provided.");
            if (group == null) throw new ArgumentNullException(nameof(@group));
            if (!phoneNumberRegex.Match(phoneNumber).Success) throw new FormatException("Invalid phone number.");

            Id = id;
            Name = name;
            Address = address;
            EmailAddress = new MailAddress(emailAddress);
            PhoneNumber = phoneNumber;
            Group = group;

            group.AddSupplier(this);
        }

        /// <summary>Updates the group for the current supplier.</summary>
        public void UpdateGroup(SupplierGroup group)
        {
            if (group != null)
            {
                Group = group;
                group.AddSupplier(this);
            }
        }
    }
}
