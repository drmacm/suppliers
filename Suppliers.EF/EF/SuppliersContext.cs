using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Suppliers.EF.DataModel;

namespace Suppliers.EF
{
    /// <summary>EntityFramework context for storing the data to database.</summary>
    public class SuppliersContext : DbContext
    {

        public SuppliersContext() : base("SuppliersContext") { }

        public virtual DbSet<SqlSupplier> Suppliers { get; set; }
        public virtual DbSet<SqlSupplierGroup> Groups { get; set; }

        /// <summary>Solves the problem with inability to mock out the DbContext.Entry.</summary>
        public virtual void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
