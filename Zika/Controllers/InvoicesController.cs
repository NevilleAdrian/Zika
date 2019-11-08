using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Zika.Models;
using Zika.Repository;
using Zika.ViewModels;

namespace Zika.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InvoicesController : Controller
    {
        private readonly InvoiceRepository _repo;
        private readonly FreightRepository _repoX;
        public InvoicesController(InvoiceRepository invoice,
            FreightRepository freight)
        {
            _repo = invoice;
            _repoX = freight;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _repo.Items());
        }

        public async Task<IActionResult> CreateAsAdmin()
        {
            ViewData["Freight"] = new SelectList(await _repoX.Items(), "FreightId", "FreightSummary");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create(int freightId)
        {
            Freight freight = await _repoX.Item(freightId);
            if(freight != null)
            {
                InvoiceViewModel ivm = new InvoiceViewModel
                {
                    Freight = freight,
                    Invoice = new Invoice()
                };
                return View(ivm);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = await _repo.Add(invoice);
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    return View("Create");
                }
                catch
                {

                }
            }
            return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _repo.Item(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Invoice invoice)
        {
            if (id != invoice.InvoiceId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = await _repo.Update(invoice);
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    return View("Edit", new { id });
                }
                catch
                {

                }
            }
            return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _repo.Item(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            try
            {
                bool result = await _repo.Delete(id);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                return View("Delete", new { id });
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }

        }
    }
}