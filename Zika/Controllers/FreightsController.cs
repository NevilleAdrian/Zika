using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Zika.Models;
using Zika.Repository;
using Zika.ViewModels;

namespace Zika.Controllers
{
    public class FreightsController : Controller
    {
        private readonly FreightRepository _repo;
        private readonly UserManager<ApplicationUser> _user;
        public FreightsController(FreightRepository freight,
            UserManager<ApplicationUser> user)
        {
            _repo = freight;
            _user = user;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _repo.Items());
        }

        
        public IActionResult Create()
        {
            ViewData["Success"] = "";
            ViewData["Address"] = "";
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateAsAdmin()
        {
            
            ViewData["Users"] = new SelectList(_user.Users, "Id", "Email");
            return View();
        }
        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Freight freight)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ViewData["Address"] = "";
                    bool result = await _repo.Add(freight);
                    if (result)
                    {
                        ViewData["Address"] = freight.FreightFrom.ToLower() == "uk" ? "UK" : "USA";
                        ViewData["Success"] = "true";
                        return View("Create");
                    }
                    ViewData["Success"] = "false";
                    return View("Create");
                }
                catch
                {

                }
            }
            return RedirectToAction("Error", "Home");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsAdmin(Freight freight)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = await _repo.Add(freight);
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    return RedirectToAction("CreateAsAdmin");
                }
                catch
                {

                }
            }
            return RedirectToAction("Error", "Home");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _repo.Item(id);
            if (model == null)
            {
                return NotFound();
            }
            ViewData["Users"] = new SelectList(_user.Users, "Id", "Email");
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Freight freight)
        {
            if (id != freight.FreightId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = await _repo.Update(freight);
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    return RedirectToAction("Edit", new { id });
                }
                catch
                {

                }
            }
            return RedirectToAction("Error", "Home");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _repo.Item(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
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

        [HttpGet]
        public async Task<IActionResult> TrackResponse(string email, int id)
        {
            RequestFreightViewModel rqv = new RequestFreightViewModel
            {
                Email = email,
                FreightId = id
            };
            string result = await _repo.TrackingInfo(rqv);
            if(!string.IsNullOrEmpty(result))
            {
                return Json(new { Data = result });
            }
            return Json(new { Data = "" });
        }
    }
}