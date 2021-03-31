using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BakeMyWorld.Website.Data;
using BakeMyWorld.Website.Data.Entities;
using BakeMyWorld.Website.Areas.Admin.Models.ViewModels;

namespace BakeMyWorld.Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CakesController : Controller
    {
        private readonly BakeMyWorldContext context;

        public CakesController(BakeMyWorldContext context)
        {
            this.context = context;
        }

        // GET: Admin/Cakes
        public async Task<IActionResult> Index()
        {
            return View(await context.Cakes.ToListAsync());
        }

        // GET: Admin/Cakes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cake = await context.Cakes
                .FirstOrDefaultAsync(m => m.Id == id);

            if (cake == null)
            {
                return NotFound();
            }

            return View(cake);
        }

        // GET: Admin/Cakes/Create
        public IActionResult Create()
        {
            // Retrieve categories for dropdown box
            var categories = context.Categories.ToList()
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name.ToString() }).ToList();

            ViewBag.Categories = categories;

            return View();
        }

        // POST: Admin/Cakes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCakeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Retrieve associated category (in case of unselection, category id will be set to default value "1")
                string categoryIdString = Request.Form["Categories"];
                bool categoryIdIsValid = int.TryParse(categoryIdString, out int categoryIdParsed);
                int categoryId = categoryIdIsValid ? categoryIdParsed : 1;
                var associatedCategory = await context.Categories.FindAsync(categoryId);

                var cake = new Cake(
                    viewModel.Name,
                    viewModel.Description,
                    viewModel.ImageUrl,
                    viewModel.Price
                    );

                cake.Categories.Add(associatedCategory);
                
                context.Add(cake);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Admin/Cakes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cake = await context.Cakes.FindAsync(id);

            if (cake == null)
            {
                return NotFound();
            }

            var viewModel = new EditCakeViewModel
            {
                Id = cake.Id,
                Name = cake.Name,
                Description = cake.Description,
                ImageUrl = cake.ImageUrl,
                Price = cake.Price
            };

            // Retrieve categories for dropdown box
            var categories = context.Categories.ToList()
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name.ToString() }).ToList();

            ViewBag.Categories = categories;

            return View(viewModel);
        }

        // POST: Admin/Cakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditCakeViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Retrieve associated category (in case of unselection, category id will be set to default value "1")
                string categoryIdString = Request.Form["Categories"];
                bool categoryIdIsValid = int.TryParse(categoryIdString, out int categoryIdParsed);
                int categoryId = categoryIdIsValid ? categoryIdParsed : 1;
                var associatedCategory = await context.Categories.FindAsync(categoryId);

                // Retrieve located cake existing categories
                var locatedCake = await context.Cakes.FindAsync(viewModel.Id);
                var locatedCakeCategories = locatedCake.Categories.ToList();

                var cake = new Cake(
                    viewModel.Id,
                    viewModel.Name,
                    viewModel.Description,
                    viewModel.ImageUrl,
                    viewModel.Price
                    );

                if (!locatedCakeCategories.Contains(associatedCategory)) cake.Categories.Add(associatedCategory);
               
                try
                {
                    context.Update(cake);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CakeExists(cake.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Admin/Cakes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cake = await context.Cakes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cake == null)
            {
                return NotFound();
            }

            return View(cake);
        }

        // POST: Admin/Cakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cake = await context.Cakes.FindAsync(id);
            context.Cakes.Remove(cake);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CakeExists(int id)
        {
            return context.Cakes.Any(e => e.Id == id);
        }
    }
}
