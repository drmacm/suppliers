using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Suppliers.Business.Business;
using Suppliers.Web.Models;

namespace Suppliers.Web.Controllers
{
    public class GroupController : Controller
    {
        private readonly SupplierGroupBll supplierGroupBll;

        /// <summary>Creates a new instance of <see cref="SupplierController"></summary>
        /// <param name="supplierGroupBll">Contains business rules related to <see cref="SupplierGroup"/> objects.</param>
        public GroupController(SupplierGroupBll supplierGroupBll)
        {
            if (supplierGroupBll == null) throw new ArgumentNullException("supplierGroupBll");

            this.supplierGroupBll = supplierGroupBll;
        }

        public ActionResult Index()
        {
            var supplierGroups = supplierGroupBll.GetAllSupplierGroups();
            var viewModel = supplierGroups.Select(g => SupplierGroupViewModel.FromSupplierGroup(g));
            return View(viewModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var supplierGroup = supplierGroupBll.GetSupplierGroup(id.Value);

            if (supplierGroup == null)
            {
                return HttpNotFound();
            }
            var viewModel = SupplierGroupViewModel.FromSupplierGroup(supplierGroup);
            return View(viewModel);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] SupplierViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                supplierGroupBll.CreateSupplierGroup(viewModel.Id, viewModel.Name);

                return RedirectToAction("Index");
            }


            return View(viewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var supplierGroup = supplierGroupBll.GetSupplierGroup(id.Value);
            if (supplierGroup == null)
            {
                return HttpNotFound();
            }

            var viewModel = SupplierGroupViewModel.FromSupplierGroup(supplierGroup);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] SupplierGroupViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                supplierGroupBll.UpdateSupplierGroup(viewModel.Id, viewModel.Name);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var supplierGroup = supplierGroupBll.GetSupplierGroup(id.Value);
            if (supplierGroup == null)
            {
                return HttpNotFound();
            }
            var viewModel = SupplierGroupViewModel.FromSupplierGroup(supplierGroup);
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            supplierGroupBll.DeleteSupplierGroup(id);
            return RedirectToAction("Index");
        }
    }
}
