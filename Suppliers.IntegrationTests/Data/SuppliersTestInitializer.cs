using System.Collections.Generic;
using System.Data.Entity;
using Suppliers.EF;
using Suppliers.EF.DataModel;

namespace Suppliers.Data
{
    /// <summary>Initializes database with test values, used for testing of data access layer.</summary>
    public class SuppliersTestInitializer : DropCreateDatabaseIfModelChanges<SuppliersContext>
    {
        protected override void Seed(SuppliersContext context)
        {
            var supplierGroups = new List<SqlSupplierGroup>
            {
                new SqlSupplierGroup {Name="TestGroup1"},
                new SqlSupplierGroup {Name="TestGroup2"},
            };
            supplierGroups.ForEach(g => context.Groups.Add(g));
            context.SaveChanges();

            var suppliers = new List<SqlSupplier>
            {
                new SqlSupplier{Name="TestSupplier1",Address="TestAddress1",EmailAddress="test.supplier1@domain.com", PhoneNumber = "723123456", GroupId=1},
                new SqlSupplier{Name="TestSupplier2",Address="TestAddress2",EmailAddress="test.supplier2@domain.com", PhoneNumber = "723123457", GroupId=1},
                new SqlSupplier{Name="TestSupplier3",Address="TestAddress3",EmailAddress="test.supplier3@domain.com", PhoneNumber = "723123458", GroupId=2},
            };

            suppliers.ForEach(s => context.Suppliers.Add(s));
            context.SaveChanges();
        }
    }
}
