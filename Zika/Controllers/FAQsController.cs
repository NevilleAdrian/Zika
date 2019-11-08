using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zika.Models;
using Zika.Repository;

namespace Zika.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FAQsController : Controller
    {
        private readonly FAQRepository _repo;
        public FAQsController(FAQRepository faq)
        {
            _repo = faq;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _repo.Items());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FAQ faq)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = await _repo.Add(faq);
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
        public async Task<IActionResult> Edit(int id, FAQ faq)
        {
            if (id != faq.FAQId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = await _repo.Update(faq);
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