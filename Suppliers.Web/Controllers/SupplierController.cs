using System.Linq;
using System.Net;
using System.Web.Mvc;
using Suppliers.Business.Business;
using Suppliers.Web.Models;

namespace Suppliers.Web.Controllers
{
    public class SupplierController : Controller
    {
        private readonly SupplierBll supplierBll;
        private readonly SupplierGroupBll supplierGroupBll;

        /// <summary>Creates a new instance of <see cref="SupplierController"></summary>
        /// <param name="supplierBll">Contains business rules related to <see cref="Supplier"/> objects.</param>
        /// <param name="supplierGroupBll">Contains business rules related to <see cref="SupplierGroup"/> objects.</param>
        public SupplierController(SupplierBll supplierBll, SupplierGroupBll supplierGroupBll)
        {
            this.supplierBll = supplierBll;
            this.supplierGroupBll = supplierGroupBll;
        }

        public ActionResult Index()
        {
            var suppliers = supplierBll.GetAllSuppliers();
            var viewModel = suppliers.Select(s => SupplierViewModel.FromSupplier(s));
            return View(viewModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var supplier = supplierBll.GetSupplier(id.Value);

            if (supplier == null)
            {
                return HttpNotFound();
            }
            var viewModel = SupplierViewModel.FromSupplier(supplier);
            return View(viewModel);
        }

        public ActionResult Create()
        {
            ViewBag.GroupId = new SelectList(supplierGroupBll.GetAllSupplierGroups(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,EmailAddress,PhoneNumber,GroupId")] SupplierViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var group = supplierGroupBll.GetSupplierGroup(viewModel.GroupId);
                supplierBll.CreateSupplier(viewModel.Id, viewModel.Name, viewModel.Address, viewModel.EmailAddress, viewModel.PhoneNumber, group);

                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(supplierGroupBll.GetAllSupplierGroups(), "Id", "Name", viewModel.GroupId);
            return View(viewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var supplier = supplierBll.GetSupplier(id.Value);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(supplierGroupBll.GetAllSupplierGroups(), "Id", "Name", supplier.Group.Id);
            var viewModel = SupplierViewModel.FromSupplier(supplier);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,EmailAddress,PhoneNumber,GroupId")] SupplierViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var group = supplierGroupBll.GetSupplierGroup(viewModel.GroupId);
                supplierBll.UpdateSupplier(viewModel.Id, viewModel.Name, viewModel.Address, viewModel.EmailAddress, viewModel.PhoneNumber, group);
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(supplierGroupBll.GetAllSupplierGroups(), "Id", "Name", viewModel.GroupId);
            return View(viewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var supplier = supplierBll.GetSupplier(id.Value);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            var viewModel = SupplierViewModel.FromSupplier(supplier);
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            supplierBll.DeleteSupplier(id);
            return RedirectToAction("Index");
        }
    }
}
