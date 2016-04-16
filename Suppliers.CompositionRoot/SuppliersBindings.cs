using Ninject.Modules;
using Suppliers.Business.IDal;
using Suppliers.DataAccess;

namespace Suppliers.CompositionRoot
{
    public class SuppliersBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ISupplierDal>().To<SupplierDal>();
            Bind<ISupplierGroupDal>().To<SupplierGroupDal>();
        }
    }
}
