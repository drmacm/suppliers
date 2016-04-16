using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Suppliers.Business.DomainModel;

namespace Suppliers.Business.Tests
{
    [TestFixture]
    public class SupplierTests
    {
        private static int id = 0;
        private static string name = "John Doe";
        private static string address = "Vaclavske Namesti 125";
        private static string emailAddress = "some.email@domain.name";
        private static string phoneNumber = "723123456";

        private SupplierGroup group = new SupplierGroup(id, "test group");

        [Test]
        public void CreateSupplier_MissingName_ShouldThrow([Values(null, "")]string emptyParam)
        {
            Assert.Throws<ArgumentException>(() => new Supplier(id, emptyParam, address, emailAddress, phoneNumber, group), "Supplier's name must be provided.");
        }

        [Test]
        public void CreateSupplier_MissingAddress_ShouldThrow([Values(null, "")]string emptyParam)
        {
            Assert.Throws<ArgumentException>(() => new Supplier(id, name, emptyParam, emailAddress, phoneNumber, group), "Supplier's address must be provided.");
        }

        [Test]
        public void CreateSupplier_MissingEmailAddress_ShouldThrow([Values(null, "")]string emptyParam)
        {
            Assert.Throws<ArgumentException>(() => new Supplier(id, name, address, emptyParam, phoneNumber, group), "Supplier's email address must be provided.");
        }

        [Test]
        public void CreateSupplier_MissingPhoneNumber_ShouldThrow([Values(null, "")]string emptyParam)
        {
            Assert.Throws<ArgumentException>(() => new Supplier(id, name, address, emailAddress, emptyParam, group), "Supplier's phone number must be provided.");
        }

        [Test]
        public void CreateSupplier_MissingGroup_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new Supplier(id, name, address, emailAddress, phoneNumber, null));
        }

        [TestCase("12345678")]
        [TestCase("1234567890")]
        [TestCase("1234a6789")]
        [TestCase("abc123456789abc")]
        public void CreateSupplier_InvalidPhoneNumber_ShouldThrow(string invalidPhoneNumber)
        {
            Assert.Throws<FormatException>(() => new Supplier(id, name, address, emailAddress, invalidPhoneNumber, group), "Invalid phone number.");
        }

        [TestCase("some.email.without.at.character")]
        [TestCase("@no.proper.start.com")]
        [TestCase("no.proper.end@")]
        public void CreateSupplier_InvalidEmailAddress_ShouldThrow(string invalidEmailAddress)
        {
            Assert.Throws<FormatException>(() => new Supplier(id, name, address, invalidEmailAddress, phoneNumber, group));
        }

        [Test]
        public void CreateSupplier_InputValid_ShouldSetAllPropertiesProperly()
        {
            var result = new Supplier(id, name, address, emailAddress, phoneNumber, group);

            Assert.AreEqual(name, result.Name);
            Assert.AreEqual(address, result.Address);
            Assert.AreEqual(emailAddress, result.EmailAddress.Address);
            Assert.AreEqual(phoneNumber, result.PhoneNumber);
            Assert.AreEqual(group.Name, result.Group.Name);

        }

        [Test]
        public void Id_InitialValue_ShouldBeZero()
        {
            var supplier = CreateSupplier(emailAddress, group);

            Assert.AreEqual(0, supplier.Id);
        }

        [Test]
        public void Id_NotYetSet_ShouldBeSet()
        {
            var supplier = CreateSupplier(emailAddress, group);
            supplier.Id = 42;

            Assert.AreEqual(42, supplier.Id);
        }

        [Test]
        public void Id_AlreadySet_ShouldThrow()
        {
            var supplier = CreateSupplier(emailAddress, group);
            supplier.Id = 42;

            Assert.Throws<InvalidOperationException>(() => supplier.Id = 43, "Supplier id can't change after initial setting.");
        }

        [Test]
        public void Id_AlreadySet_CanBeSetToSameValue()
        {
            var supplier = CreateSupplier(emailAddress, group);
            supplier.Id = 42;
            supplier.Id = 42;

            Assert.AreEqual(42, supplier.Id);
        }

        [Test]
        public void UpdateGroup_GroupNull_ShouldKeepCurrentGroup()
        {
            var supplier = CreateSupplier(emailAddress, group);

            supplier.UpdateGroup(null);

            Assert.AreEqual(group.Name, supplier.Group.Name);
        }

        #region Helper methods
        public static Supplier CreateSupplier(string email, SupplierGroup group)
        {
            return new Supplier(id, name, address, email, phoneNumber, group);
        }
        #endregion
    }
}
