using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BakeMyWorld.Website.Data;
using BakeMyWorld.Website.Data.Entities;

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
            return View();
        }

        // POST: Admin/Cakes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ImageUrl,Price,UrlSlug")] Cake cake)
        {
            if (ModelState.IsValid)
            {
                context.Add(cake);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cake);
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
            return View(cake);
        }

        // POST: Admin/Cakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ImageUrl,Price,UrlSlug")] Cake cake)
        {
            if (id != cake.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            return View(cake);
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
