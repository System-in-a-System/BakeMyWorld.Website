using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BakeMyWorld.Website.Data;

namespace BakeMyWorld.Website.Controllers
{
    public class GiftBoxController : Controller
    {
        private readonly BakeMyWorldContext context;

        public GiftBoxController(BakeMyWorldContext context)
        {
            this.context = context;
        }

        // GET /GiftBox/cake
        [Route("/giftBoxes/{urlSlug}", Name = "giftboxdetails")]
        public async Task<IActionResult> Details(string urlSlug)
        {
            if (urlSlug == "")
            {
                return NotFound();
            }

            var giftbox = await context.GiftBoxes
                .FirstOrDefaultAsync(m => m.UrlSlug == urlSlug);
            if (giftbox == null)
            {
                return NotFound();
            }

            return View(giftbox);
        }
    }
}
