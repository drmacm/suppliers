using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Suppliers.Business.DomainModel;
using Suppliers.Business.IDal;
using Suppliers.DataAccess.Tests.DataModel;

namespace Suppliers.DataAccess.Tests.Dal
{
    [TestFixture]
    public class SupplierGroupDalTests
    {
        private int supplierGroupId = 101;
        private FakeDbSet<SqlSupplierGroup> groups;
        private Mock<SuppliersContext> context;

        private ISupplierGroupDal supplierGroupDal;

        [SetUp]
        public void SetUp()
        {
            var groupData = new List<SqlSupplierGroup> { SqlSupplierGroupTests.CreateSqlSupplierGroup()}.AsQueryable();
            groups = new FakeDbSet<SqlSupplierGroup>(groupData);
            
            context = new Mock<SuppliersContext>();
            context.Setup(m => m.Groups).Returns(groups.Object);

            supplierGroupDal = new SupplierGroupDal(context.Object);
        }

        [Test]
        public void GetAll_NoGroups_ShouldReturnEmptyList()
        {
            context.Setup(m => m.Groups).Returns(new FakeDbSet<SqlSupplierGroup>(new List<SqlSupplierGroup>()).Object);

            var result = supplierGroupDal.GetAll();

            Assert.IsEmpty(result);
        }

        [Test]
        public void GetAll_HasGroups_ShouldReturnAllGroups()
        {
            var result = supplierGroupDal.GetAll();

            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GetOne_NonExistingGroup_ShouldThrow()
        {
            Assert.Throws<ArgumentException>(() => supplierGroupDal.GetOne(19), "Supplier group with id 19 does not exist");
        }

        [Test]
        public void GetOne_ExistingGroup_ShouldReturnSupplier()
        {
            var result = supplierGroupDal.GetOne(supplierGroupId);

            Assert.NotNull(result);
        }

        [Test]
        public void Create_ValidGroup_ShouldStoreGroupViaContext()
        {
            supplierGroupDal.Create(new SupplierGroup(102, "some name"));

            groups.Verify(s => s.Add(It.IsAny<SqlSupplierGroup>()), Times.Once);
            context.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public void Delete_ValidGroup_ShouldRemoveGroupViaContext()
        {
            supplierGroupDal.Delete(supplierGroupId);

            groups.Verify(s => s.Remove(It.IsAny<SqlSupplierGroup>()), Times.Once);
            context.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public void Update_ValidGroup_ShouldUpdateGroupViaContext()
        {
            var group = supplierGroupDal.GetOne(supplierGroupId);
            var newName = group.Name + " - updated";
            var updatedGroup = new SupplierGroup(group.Id, newName);

            supplierGroupDal.Update(updatedGroup);

            context.Verify(c => c.SetModified(It.IsAny<object>()));
            context.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}
