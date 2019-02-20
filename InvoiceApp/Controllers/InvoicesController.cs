using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InvoiceApp.Models;
using InvoiceApp.ViewModels;
using Microsoft.Ajax.Utilities;

namespace InvoiceApp.Controllers
{
    public class InvoicesController : Controller
    {
        private MyDbContext db = new MyDbContext();



        // GET: Invoices
        public async Task<ActionResult> Index() //DateTime? StartDate, DateTime? EndDate
        {
            ViewBag.ActiveMenu = "Invoices"; // Active Menu is used to set the Navbar .active Class

            return View(await db.Invoices.Include(i => i.Customer).ToListAsync());

            //ViewBag.Datetime = DateTime.Now;

            //if (StartDate != null && EndDate != null)
            //{
            //    return View(await db.Invoices.Where(x => x.Date > StartDate.Value && x.Date <= EndDate).Include(i => i.Customer).ToListAsync());
            //}
            //else
            //{
            //    var InvoicesList = await db.Invoices.Include(i => i.Customer).ToListAsync();
            //    var reportVM = new ReportViewModel
            //    {
            //        Invoices = InvoicesList,
            //        StartDate = null,
            //        EndDate = null,
            //    };

            //    return View();
            //}
        }

        // GET: Invoices/Details/5
        public async Task<ActionResult> Details(int? id)
        {

            var invoiceItems = db.InvoiceItems.Where(x => x.InvoiceId == id).Include(i => i.Product).ToList();


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var invoice = await db.Invoices.Include(i => i.Customer).FirstAsync(x => x.Id == id);

            if (invoice == null)
            {
                return HttpNotFound();
            }

            InvoicesViewModel invoicesViewModel = new InvoicesViewModel
            {
                Invoice = invoice,
                InvoiceItems = invoiceItems,
            };

            return View(invoicesViewModel);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            ViewBag.ActiveMenu = "Create";

            var customers = db.Customers.ToList();
            var products = db.Products.ToList();

            var date = DateTime.Now;

            List<InvoiceItem> newInvoiceItems =
                new List<InvoiceItem> {
                    new InvoiceItem(),
                    new InvoiceItem(),
                    new InvoiceItem()
                };

            InvoicesViewModel invoicesViewModel = new InvoicesViewModel()
            {
                Invoice = new Invoice(),
                Customers = customers,
                Products = products,
                InvoiceItems = newInvoiceItems,
            };


            return View(invoicesViewModel);
        }

        // POST: Invoices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(InvoicesViewModel viewModel)
        {

            if (viewModel.Invoice.Date == null)
            {
                viewModel.Invoice.Date = DateTime.Now;
            }

            var newInvoice = new Invoice
            {
                CustomerId = viewModel.Invoice.CustomerId,
                Description = viewModel.Invoice.Description,
                PaymentMethod = viewModel.Invoice.PaymentMethod,
                Date = viewModel.Invoice.Date,
                Customer = db.Customers.Find(viewModel.Invoice.CustomerId),
            };

            var newInvoiceId = db.Invoices.Add(newInvoice).Id;

            decimal total = 0;

            foreach (var item in viewModel.InvoiceItems)
            {
                Product product = db.Products.Find(item.ProductId);

                if (product != null && product.Id != 0)
                {
                    var newInvoiceItem = new InvoiceItem()
                    {
                        InvoiceId = newInvoiceId,
                        ProductId = product.Id,
                        Qty = item.Qty,
                        Price = product.Price,
                    };

                    total += product.Price * item.Qty;

                    db.InvoiceItems.Add(newInvoiceItem);
                }
                else
                {
                    Debug.WriteLine("No item Selected");
                }

            }

            newInvoice.Id = newInvoiceId;
            newInvoice.Amount = total;

            db.Invoices.AddOrUpdate(newInvoice);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return RedirectToAction("Details", new { id = newInvoice.Id });
        }

        // GET: Invoices/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Invoice invoice = await db.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Date,Description,PaymentMethod")]
            Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Invoice invoice = await db.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Invoice invoice = await db.Invoices.FindAsync(id);
            db.Invoices.Remove(invoice);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
