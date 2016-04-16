using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Suppliers.Business.DomainModel;
using Suppliers.EF.DataModel;

namespace Suppliers.EF.Tests.DataModel
{
    [TestFixture]
    public class SqlSupplierGroupTests
    {
        private static int supplierGroupId = 101;
        private static string supplierGroupName = "Initial supplier group";

        [Test]
        public void FromSupplierGroup_SupplierNull_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => SqlSupplierGroup.FromSupplierGroup(null), "Supplier group can't be null.");
        }

        [Test]
        public void FromSupplierGroup_ValidInput_ShouldReturnSqlSupplierGroupWithCopiedValues()
        {
            var group = new SupplierGroup(supplierGroupId, supplierGroupName);
            var result = SqlSupplierGroup.FromSupplierGroup(group);

            Assert.NotNull(result);

            Assert.AreEqual(supplierGroupId, result.Id);
            Assert.AreEqual(supplierGroupName, result.Name);
        }

        [Test]
        public void ToSupplierGroup_PropertiesHaveProperValues_ShouldReturnSupplierGroupWithCopiedValues()
        {
            var sqlSupplierGroup = CreateSqlSupplierGroup();
            var result = sqlSupplierGroup.ToSupplierGroup();

            Assert.NotNull(result);

            Assert.AreEqual(supplierGroupId, result.Id);
            Assert.AreEqual(supplierGroupName, result.Name);
            Assert.AreEqual(1, result.Suppliers.Count);
        }

        #region Helper methods
        public static SqlSupplierGroup CreateSqlSupplierGroup()
        {
            var sqlSupplierGroup = new SqlSupplierGroup
            {
                Id = supplierGroupId,
                Name = supplierGroupName,
                Suppliers = new List<SqlSupplier>()
            };
            sqlSupplierGroup.Suppliers.Add(SqlSupplierTests.CreateSqlSupplier());

            return sqlSupplierGroup;
        }
        #endregion
    }
}
