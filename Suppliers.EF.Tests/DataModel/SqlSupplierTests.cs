using System;
using NUnit.Framework;
using Suppliers.Business.DomainModel;
using Suppliers.Business.Tests;
using Suppliers.EF.DataModel;

namespace Suppliers.EF.Tests.DataModel
{
    [TestFixture]
    public class SqlSupplierTests
    {
        [Test]
        public void FromSupplier_SupplierNull_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => SqlSupplier.FromSupplier(null), "Supplier can't be null.");
        }

        [Test]
        public void FromSupplier_ValidInput_ShouldReturnSqlSupplierWithCopiedValues()
        {
            var group = new SupplierGroup(101, "Initial supplier group");
            var supplier = SupplierTests.CreateSupplier("some.email@domain.name", group);

            var result = SqlSupplier.FromSupplier(supplier);

            Assert.NotNull(result);

            Assert.AreEqual(0, result.Id);
            Assert.AreEqual("John Doe", result.Name);
            Assert.AreEqual("Vaclavske Namesti 125", result.Address);
            Assert.AreEqual("some.email@domain.name", result.EmailAddress);
            Assert.AreEqual("723123456", result.PhoneNumber);
            Assert.AreEqual(101, result.GroupId);
        }

        [Test]
        public void ToSupplier_PropertiesHaveProperValues_ShouldReturnSupplierWithCopiedValues()
        {
            var sqlSupplier = CreateSqlSupplier();

            var result = sqlSupplier.ToSupplier();

            Assert.NotNull(result);
            Assert.NotNull(result.Group);
            Assert.AreEqual(42, result.Id);
            Assert.AreEqual("John Doe", result.Name);
            Assert.AreEqual("Vaclavske Namesti 125", result.Address);
            Assert.AreEqual("some.email@domain.name", result.EmailAddress.Address);
            Assert.AreEqual("723123456", result.PhoneNumber);
            Assert.AreEqual("Initial supplier group", result.Group.Name);
        }

        #region Helper methods
        public static SqlSupplier CreateSqlSupplier()
        {
            return new SqlSupplier
            {
                Id = 42,
                Name = "John Doe",
                Address = "Vaclavske Namesti 125",
                EmailAddress = "some.email@domain.name",
                PhoneNumber = "723123456",
                GroupId = 101,
                Group = new SqlSupplierGroup
                {
                    Id = 101,
                    Name = "Initial supplier group"
                }
            };
        }
        #endregion
    }
}
