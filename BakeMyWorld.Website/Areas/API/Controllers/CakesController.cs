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
    [ApiVersion("1")]
    [ApiController]

    public class CakesController : ControllerBase
    {
        private readonly BakeMyWorldContext context;

        public CakesController(BakeMyWorldContext context)
        {
            this.context = context;
        }

        // GET: api/Cakes
        /// <summary>
        /// Provides the List of Cakes
        /// </summary>
        /// <returns>Cakes</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cake>>> GetCakes()
        {
            return await context.Cakes.ToListAsync();
        }

        // GET: api/Cakes/5
        /// <summary>
        /// Provides a specific Cake based on id number
        /// </summary>
        /// <returns>Cake</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <summary>
        /// Updates a specific Cake based on id number
        /// </summary>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

            //var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == cakeDto.CategoryId);
            //cake.Categories.Add(category);

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
        /// <summary>
        /// Registers new Cake
        /// </summary>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("[action]")]
      
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

        // POST: api/Cakes/CakeToCorporate
        /// <summary>
        /// Registers new Cake to Corporate
        /// </summary>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<Cake>> PostCakeToCorporate(CakeDto cakeDto)
        {
            var cake = new Cake(
                cakeDto.Id,
                cakeDto.Name,
                cakeDto.Description,
                cakeDto.ImageUrl,
                cakeDto.Price
                );

            var corporate = await context.Corporates.FirstOrDefaultAsync(c => c.Id == cakeDto.CorporateId);
            var corporateId = cakeDto.CorporateId;
            cake.Corporates.Add(corporate);

            context.Cakes.Add(cake);

            await context.SaveChangesAsync();

            var cakedto = new Cake (corporateId);

            return CreatedAtAction("GetCake", new { id = cake.Id }, cakedto);
        }
        // DELETE: api/Cakes/5
        /// <summary>
        /// Deletes a specific Cake based on id number
        /// </summary>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [ApiExplorerSettings(IgnoreApi = true)]
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
