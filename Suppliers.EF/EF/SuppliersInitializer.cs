using System.Collections.Generic;
using System.Data.Entity;
using Suppliers.EF.DataModel;

namespace Suppliers.EF
{
    public class SuppliersInitializer : DropCreateDatabaseIfModelChanges<SuppliersContext>
    {
        protected override void Seed(SuppliersContext context)
        {
            var supplierGroups = new List<SqlSupplierGroup>
            {
                new SqlSupplierGroup {Name="Group1"},
                new SqlSupplierGroup {Name="Group2"},
            };
            supplierGroups.ForEach(g => context.Groups.Add(g));
            context.SaveChanges();

            var suppliers = new List<SqlSupplier>
            {
                new SqlSupplier{Name="S1",Address="A1",EmailAddress="a1@domain.com", PhoneNumber = "000000001", GroupId=1},
                new SqlSupplier{Name="S2",Address="A2",EmailAddress="a2@domain.com", PhoneNumber = "000000002", GroupId=1},
                new SqlSupplier{Name="S3",Address="A3",EmailAddress="a3@domain.com", PhoneNumber = "000000003", GroupId=2},
            };

            suppliers.ForEach(s => context.Suppliers.Add(s));
            context.SaveChanges();
        }
    }
}
