using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zika.Models;
using Zika.Repository;
using Zika.Services;

namespace Zika.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailSender _email;
        public HomeController(IEmailSender email) {
            _email = email;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Terms()
        {
            

            return View();
        }

        public IActionResult Disclaimer()
        {

            return View();
        }

        public IActionResult Privacy()
        {

            return View();
        }



        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public async Task<IActionResult> ContactResolve(string name, string email, string message)
        {
            try
            {
                await _email.SendEmailAsync("zikaexpress01@gmail.com", "CONTACT INFORMATION", message);
                return Json(new { Data = true });
            }
            catch
            {
                return Json(new { Data = false });
            }
        }

        public IActionResult Team()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }
        public IActionResult Exchange()
        {
            return View();
        }

        public IActionResult RequestFreight()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
