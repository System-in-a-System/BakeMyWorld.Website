using BakeMyWorld.Website.Data;
using BakeMyWorld.Website.Extensions;
using BakeMyWorld.Website.Models.Domain;
using BakeMyWorld.Website.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakeMyWorld.Website.Controllers
{
    
    public class OrderController : Controller
    {
        private readonly BakeMyWorldContext context;

        public OrderController(BakeMyWorldContext context)
        {
            this.context = context;
        }

        [Route("/checkout")]
        public IActionResult Checkout()
        {
            var viewModel = new CheckOutViewModel
            {
                Cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart()
            };
             
            return View(viewModel);
        }

        [HttpPost]
        [Route ("/checkout")]
        public IActionResult Checkout(CheckOutViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                viewModel.Cart = HttpContext.Session.Get<Cart>("Cart");
                return View(viewModel);
            }

            return RedirectToAction(nameof(Confirmation));
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
