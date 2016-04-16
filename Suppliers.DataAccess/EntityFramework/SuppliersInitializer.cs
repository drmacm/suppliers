using System.Collections.Generic;
using System.Data.Entity;

namespace Suppliers.DataAccess
{
    public class SuppliersInitializer : DropCreateDatabaseIfModelChanges<SuppliersContext>
    {
        protected override void Seed(SuppliersContext context)
        {
            var supplierGroups = new List<SqlSupplierGroup>
            {
                new SqlSupplierGroup {Name="Cleaners"},
                new SqlSupplierGroup {Name="Office supply (paper, envelopes, etc)"},
                new SqlSupplierGroup {Name="Telephone service"},
                new SqlSupplierGroup {Name="Security"},
            };
            supplierGroups.ForEach(g => context.Groups.Add(g));
            context.SaveChanges();

            var suppliers = new List<SqlSupplier>
            {
                new SqlSupplier{Name="Úklidové služby", Address="Vrchlického 10/42, Praha", EmailAddress="uklidovesluzby@suppliers.com", PhoneNumber = "608420994", GroupId=1},
                new SqlSupplier{Name="Mytí oken Praha", Address="Hvězdova 1566/21, Praha", EmailAddress="mytioken@suppliers.com", PhoneNumber = "605827609", GroupId=1},
                new SqlSupplier{Name="Office Products, s.r.o.", Address="Křižíkova 123/69, Praha", EmailAddress="officeproducts@suppliers.com", PhoneNumber = "775535333", GroupId=2},
                new SqlSupplier{Name="SmartTel+", Address="10126, J. Vilmsi tn. 47, Tallin", EmailAddress="smarttel@suppliers.com", PhoneNumber = "234280677", GroupId=2},
                new SqlSupplier{Name="SECURITAS ČR s.r.o.", Address="Pod pekárnami 878/2, Praha", EmailAddress="securitas@suppliers.com", PhoneNumber = "284017111", GroupId=1},
            };

            suppliers.ForEach(s => context.Suppliers.Add(s));
            context.SaveChanges();
        }
    }
}
