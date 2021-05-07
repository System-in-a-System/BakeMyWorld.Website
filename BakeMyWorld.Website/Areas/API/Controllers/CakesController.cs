using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BakeMyWorld.Website.Data;
using BakeMyWorld.Website.Data.Entities;
using BakeMyWorld.Website.Areas.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BakeMyWorld.Website.Areas.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CakesController : ControllerBase
    {
        private readonly BakeMyWorldContext context;

        public CakesController(BakeMyWorldContext context)
        {
            this.context = context;
        }

        // GET: api/Cakes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cake>>> GetCakes()
        {
            return await context.Cakes.ToListAsync();
        }

        // GET: api/Cakes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cake>> GetCake(int id)
        {
            var cake = await context.Cakes.FindAsync(id);

            if (cake == null)
            {
                return NotFound();
            }

            return cake;
        }

        // PUT: api/Cakes/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCake(int id, CakeDto cakeDto)
        {
            if (id != cakeDto.Id)
            {
                return BadRequest();
            }

            var cake = new Cake(
                cakeDto.Id,
                cakeDto.Name,
                cakeDto.Description,
                cakeDto.ImageUrl,
                cakeDto.Price
                );

            var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == cakeDto.CategoryId);
            cake.Categories.Add(category);

            context.Entry(cake).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CakeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cakes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<Cake>> PostCake(CakeDto cakeDto)
        {
            var cake = new Cake(
                cakeDto.Id,
                cakeDto.Name,
                cakeDto.Description,
                cakeDto.ImageUrl,
                cakeDto.Price
                );

            var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == cakeDto.CategoryId);
            cake.Categories.Add(category);
            
            context.Cakes.Add(cake);
            await context.SaveChangesAsync();

            var cakedto = ToCakeDto(cake);
                             
            return CreatedAtAction("GetCake", new { id = cakedto.Id }, cakedto);
        }

        // DELETE: api/Cakes/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCake(int id)
        {
            var cake = await context.Cakes.FindAsync(id);
            if (cake == null)
            {
                return NotFound();
            }

            context.Cakes.Remove(cake);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool CakeExists(int id)
        {
            return context.Cakes.Any(e => e.Id == id);
        }


        public CakeDto ToCakeDto(Cake cake)
           => new CakeDto
           {
               Id = cake.Id,
               Name = cake.Name,
               Description = cake.Description,
               ImageUrl = cake.ImageUrl,
               Price = cake.Price,
               CategoryId = cake.Categories.FirstOrDefault().Id,
           };
    }
}
