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
    public class SupplierEFTests
    {
        private int supplierId = 42;
        private FakeDbSet<SqlSupplier> suppliers;
        private Mock<SuppliersContext> context;

        private ISupplierDal supplierDal;

        [SetUp]
        public void SetUp()
        {
            var supplierData = new List<SqlSupplier> { SqlSupplierTests.CreateSqlSupplier()}.AsQueryable();
            suppliers = new FakeDbSet<SqlSupplier>(supplierData);
            
            context = new Mock<SuppliersContext>();
            context.Setup(m => m.Suppliers).Returns(suppliers.Object);

            supplierDal = new SupplierDal(context.Object);
        }

        [Test]
        public void GetAll_NoSuppliers_ShouldReturnEmptyList()
        {
            context.Setup(m => m.Suppliers).Returns(new FakeDbSet<SqlSupplier>(new List<SqlSupplier>()).Object);
            var result = supplierDal.GetAll();

            Assert.IsEmpty(result);
        }

        [Test]
        public void GetAll_HasSuppliers_ShouldReturnAllSuppliers()
        {
            var result = supplierDal.GetAll();

            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GetOne_NonExistingSupplier_ShouldThrow()
        {
            Assert.Throws<ArgumentException>(() => supplierDal.GetOne(19), "Supplier with id 19 does not exist");
        }

        [Test]
        public void GetOne_ExistingSupplier_ShouldReturnSupplier()
        {
            var result = supplierDal.GetOne(supplierId);

            Assert.NotNull(result);
        }

        [Test]
        public void Create_ValidSupplier_ShouldStoreSupplierViaContext()
        {
            supplierDal.Create(new Supplier(43, "some name", "address", "e.mail@addre.ss", "123123123", new SupplierGroup(102, "name")));

            suppliers.Verify(s => s.Add(It.IsAny<SqlSupplier>()), Times.Once);
            context.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public void Delete_ValidSupplier_ShouldRemoveSupplierViaContext()
        {
            supplierDal.Delete(supplierId);

            suppliers.Verify(s => s.Remove(It.IsAny<SqlSupplier>()), Times.Once);
            context.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public void Update_ValidSupplier_ShouldUpdateSupplierViaContext()
        {
            var sup = supplierDal.GetOne(supplierId);
            var newName = sup.Name + " - updated";
            var updatedSupplier = new Supplier(sup.Id, newName, sup.Address, sup.EmailAddress.Address, sup.PhoneNumber, sup.Group);

            supplierDal.Update(updatedSupplier);

            context.Verify(c => c.SetModified(It.IsAny<object>()));
            context.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}
