using Moq;
using NUnit.Framework;
using Suppliers.Business.Business;
using Suppliers.Business.DomainModel;
using Suppliers.Business.IDal;

namespace Suppliers.Business.Tests.Bll
{
    [TestFixture]
    public class SupplierGroupBllTests
    {
        private Mock<ISupplierGroupDal> supplierGroupDal;
        private SupplierGroupBll supplierGroupBll;

        [SetUp]
        public void SetUp()
        {
            supplierGroupDal = new Mock<ISupplierGroupDal>();
            supplierGroupBll = new SupplierGroupBll(supplierGroupDal.Object);
        }

        [Test]
        public void GetAllSupplierGroups_ShouldCallAppropriateDalMethod()
        {
            supplierGroupBll.GetAllSupplierGroups();

            supplierGroupDal.Verify(s => s.GetAll(), Times.Once);
        }

        [Test]
        public void GetSupplierGroup_ShouldCallAppropriateDalMethod()
        {
            supplierGroupBll.GetSupplierGroup(103);

            supplierGroupDal.Verify(s => s.GetOne(103), Times.Once);
        }

        [Test]
        public void CreateSupplierGroup_ShouldCallAppropriateDalMethod()
        {
            supplierGroupBll.CreateSupplierGroup(13, "name");

            supplierGroupDal.Verify(s => s.Create(It.IsAny<SupplierGroup>()), Times.Once);
        }

        [Test]
        public void UpdateSupplierGroup_ShouldCallAppropriateDalMethod()
        {
            supplierGroupBll.UpdateSupplierGroup(13, "name");

            supplierGroupDal.Verify(s => s.Update(It.IsAny<SupplierGroup>()), Times.Once);
        }

        [Test]
        public void DeleteSupplierGroup_ShouldCallAppropriateDalMethod()
        {
            supplierGroupBll.DeleteSupplierGroup(13);

            supplierGroupDal.Verify(s => s.Delete(13), Times.Once);
        }
    }
}
