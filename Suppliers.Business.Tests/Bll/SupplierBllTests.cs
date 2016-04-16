using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Suppliers.Business.Business;
using Suppliers.Business.DomainModel;
using Suppliers.Business.IDal;

namespace Suppliers.Business.Tests.Bll
{
    [TestFixture]
    public class SupplierBllTests
    {
        private Mock<ISupplierDal> supplierDal;
        private SupplierBll supplierBll;

        [SetUp]
        public void SetUp()
        {
            supplierDal = new Mock<ISupplierDal>();
            supplierBll = new SupplierBll(supplierDal.Object);
        }

        [Test]
        public void GetAllSuppliers_ShouldCallAppropriateDalMethod()
        {
            supplierBll.GetAllSuppliers();

            supplierDal.Verify(s => s.GetAll(), Times.Once);
        }

        [Test]
        public void GetSupplier_ShouldCallAppropriateDalMethod()
        {
            supplierBll.GetSupplier(103);

            supplierDal.Verify(s => s.GetOne(103), Times.Once);
        }

        [Test]
        public void CreateSupplier_ShouldCallAppropriateDalMethod()
        {
            supplierBll.CreateSupplier(13, "name", "address", "em.ail@addre.ss", "123123123", new SupplierGroup(1,"name"));

            supplierDal.Verify(s => s.Create(It.IsAny<Supplier>()), Times.Once);
        }

        [Test]
        public void UpdateSupplier_ShouldCallAppropriateDalMethod()
        {
            supplierBll.UpdateSupplier(13, "name", "address", "em.ail@addre.ss", "123123123", new SupplierGroup(1, "name"));

            supplierDal.Verify(s => s.Update(It.IsAny<Supplier>()), Times.Once);
        }

        [Test]
        public void DeleteSupplier_ShouldCallAppropriateDalMethod()
        {
            supplierBll.DeleteSupplier(13);

            supplierDal.Verify(s => s.Delete(13), Times.Once);
        }
    }
}
