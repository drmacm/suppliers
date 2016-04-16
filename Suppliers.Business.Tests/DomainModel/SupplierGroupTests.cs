using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Suppliers.Business.DomainModel;

namespace Suppliers.Business.Tests.DomainModel
{
    [TestFixture]
    public class SupplierGroupTests
    {
        private int id = 0;
        private string name = "Test Group";
        private SupplierGroup group;

        [SetUp]
        public void SetUp()
        {
            group = new SupplierGroup(id, name);
        }

        [Test]
        public void CreateSupplierGroup_MissingName_ShouldThrow([Values(null, "")] string emptyParam)
        {
            Assert.Throws<ArgumentException>(() => new SupplierGroup(id, emptyParam), "Supplier group's name must be provided.");
        }

        [Test]
        public void CreateSupplierGroup_InputValid_ShouldSetAllPropertiesProperly()
        {
            Assert.AreEqual(name, group.Name);
            Assert.IsNotNull(@group.Suppliers);
            Assert.IsEmpty(@group.Suppliers);
        }

        [Test]
        public void Id_InitialValue_ShouldBeZero()
        {
            Assert.AreEqual(0, group.Id);
        }

        [Test]
        public void Id_NotYetSet_ShouldBeSet()
        {
            group.Id = 42;

            Assert.AreEqual(42, group.Id);
        }

        [Test]
        public void Id_AlreadySet_ShouldThrow()
        {
            group.Id = 42;

            Assert.Throws<InvalidOperationException>(() => group.Id = 43, "Supplier group id can't change after initial setting.");
        }

        [Test]
        public void Id_AlreadySet_CanBeSetToSameValue()
        {
            group.Id = 42;
            group.Id = 42;

            Assert.AreEqual(42, group.Id);
        }

        [Test]
        public void AddSupplier_SupplierNull_ShouldReturnFalse()
        {
            var result = group.AddSupplier(null);

            Assert.False(result);
        }

        [Test]
        public void AddSupplierCalledViaConstructor_ShouldAddSupplierToCollection()
        {
            SupplierTests.CreateSupplier("supplier1@domain.com", group);
            SupplierTests.CreateSupplier("supplier2@domain.com", group);
            SupplierTests.CreateSupplier("supplier3@domain.com", group);

            Assert.IsNotEmpty(@group.Suppliers);
            Assert.AreEqual(3, @group.Suppliers.Count);
        }

        [Test]
        public void AddSupplierCalledViaConstructor_DuplicatedSuppliers_ShouldAddOnlyOneSupplier()
        {
            SupplierTests.CreateSupplier("supplier1@domain.com", group);
            SupplierTests.CreateSupplier("supplier1@domain.com", group);
            SupplierTests.CreateSupplier("supplier1@domain.com", group);

            Assert.IsNotEmpty(group.Suppliers);
            Assert.AreEqual(1, group.Suppliers.Count);
        }

        [Test]
        public void AddSupplier_SupplierBelongsToDifferentGroup_ShouldAddAndUpdateSupplier()
        {
            var tempGroup = new SupplierGroup(id, "temp group");
            var supplier = SupplierTests.CreateSupplier("supplier1@domain.com", tempGroup); //at this moment, supplier belongs to tempgroup - assigned in constructor

            group.AddSupplier(supplier);

            Assert.AreEqual(1, group.Suppliers.Count);
            Assert.AreEqual(group.Name, supplier.Group.Name);
        }
    }
}
