using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zika.Models;
using Zika.Repository;
using Zika.ViewModels;

namespace Zika.Controllers
{
    
    public class UsersController : Controller
    {
        private readonly UsersRepository _repo;
        public UsersController(UsersRepository user)
        {
            _repo = user;
        }
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string viewInfo  = null)
        {
            ViewData["Email"] = viewInfo ?? "";
            return View(await _repo.Items());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Email email)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = await _repo.Add(email);
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
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
        public async Task<IActionResult> Edit(int id, Email email)
        {
            if (id != email.EmailId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = await _repo.Update(email);
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
        [Authorize(Roles = "Admin")]
        public IActionResult SendEmailToList()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendEmailToList(EmailViewModel evm)
        {

            try
            {
                bool result = await _repo.SendEmailsToList(evm.Message, evm.Subject);
                if (result)
                {
                    return RedirectToAction("Index", new { viewInfo = "success" });
                }
                return RedirectToAction("Index", new { viewInfo = "success" });
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }

        }
        [Authorize(Roles = "Admin")]
        public IActionResult SendEmailToAll()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendEmailToAll(EmailViewModel evm)
        {

            try
            {
                bool result = await _repo.SendEmailsToAll(evm.Message, evm.Subject);
                if (result)
                {
                    ViewData["Email"] = "success";
                    return RedirectToAction("Index");
                }
                ViewData["Email"] = "failed";
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }

        }
    }
}