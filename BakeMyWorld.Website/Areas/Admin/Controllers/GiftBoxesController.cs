using BakeMyWorld.Website.Areas.Admin.Models.ViewModels;
using BakeMyWorld.Website.Data;
using BakeMyWorld.Website.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BakeMyWorld.Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GiftBoxesController : Controller
    {
        private readonly BakeMyWorldContext context;

        public GiftBoxesController(BakeMyWorldContext context)
        {
            this.context = context;
        }
        // GET: /Admin/GiftBoxes
        public async Task<ActionResult> Index()
        {
            return View(await context.GiftBoxes.ToListAsync());

        }

        // GET: /Admin/GiftBoxes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giftBox = await context.GiftBoxes
                .FirstOrDefaultAsync(m => m.Id == id);

            if (giftBox == null)
            {
                return NotFound();
            }

            return View(giftBox);
        
        }

        // GET: /Admin/GiftBoxes/Create
        public ActionResult Create()
        {
            var corporates = context.Corporates.ToList()
               .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name.ToString() }).ToList();

            ViewBag.Corporates = corporates;

            return View();
        }

        // POST: GiftBoxes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateGiftBoxViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Retrieve associated category (in case of unselection, category id will be set to default value "0")
                string corporateIdString = Request.Form["Corporates"];
                bool categoryIdIsValid = int.TryParse(corporateIdString, out int corporateIdParsed);
                int corporateId = categoryIdIsValid ? corporateIdParsed : 0;
                var associatedCorporate = context.Corporates.Find(corporateId);

                var giftBox = new GiftBox(
                    viewModel.Name,
                    viewModel.Description,
                    viewModel.ImageUrl,
                    viewModel.Price
                    );

                if (associatedCorporate != null) giftBox.Corporates.Add(associatedCorporate);

                context.Add(giftBox);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: /Admin/GiftBoxes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giftBox = await context.GiftBoxes.FindAsync(id);

            if (giftBox == null)
            {
                return NotFound();
            }

            var viewModel = new EditGiftBoxViewModel
            {
                Id = giftBox.Id,
                Name = giftBox.Name,
                Description = giftBox.Description,
                ImageUrl = giftBox.ImageUrl,
                Price = giftBox.Price
            };

            var corporates = context.Corporates.ToList()
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name.ToString() }).ToList();

            ViewBag.Corporates = corporates;

            return View(viewModel);
        }

        // POST: /Admin/GiftBoxes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EditGiftBoxViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
             
                string corporateIdString = Request.Form["Corporates"];
                bool corporateIdIsValid = int.TryParse(corporateIdString, out int corporateIdParsed);
                int corporateId = corporateIdIsValid ? corporateIdParsed : 0;
                var associatedCorporate = await context.Corporates.FindAsync(corporateId);

        
                var locatedGiftBox = await context.GiftBoxes.Include(c => c.Corporates).FirstOrDefaultAsync(c => c.Id == viewModel.Id);
                var locatedGiftBoxCorporate = locatedGiftBox.Corporates.ToList();

                locatedGiftBox.Name = viewModel.Name;
                locatedGiftBox.Description = viewModel.Description;
                locatedGiftBox.ImageUrl = viewModel.ImageUrl;
                locatedGiftBox.Price = viewModel.Price;

             
                if (associatedCorporate != null &&
                    !locatedGiftBoxCorporate.Contains(associatedCorporate))
                {
                    locatedGiftBox.Corporates.Add(associatedCorporate);
                }


                try
                {
                    context.Update(locatedGiftBox);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiftBoxExists(locatedGiftBox.Id))
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

        // GET: /Admin/GiftBoxes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giftBox = await context.GiftBoxes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (giftBox == null)
            {
                return NotFound();
            }

            return View(giftBox);
        }

        // POST: /Admin/GiftBoxes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giftBox = await context.GiftBoxes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (giftBox == null)
            {
                return NotFound();
            }

            return View(giftBox);
        }

        // POST: Admin/GiftBoxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var giftBox = await context.GiftBoxes.FindAsync(id);
            context.GiftBoxes.Remove(giftBox);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiftBoxExists(int id)
        {
            return context.GiftBoxes.Any(e => e.Id == id);
        }
    }
}
