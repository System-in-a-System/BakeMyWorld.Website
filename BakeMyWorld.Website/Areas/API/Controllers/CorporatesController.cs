using BakeMyWorld.Website.Areas.API.Models;
using BakeMyWorld.Website.Data;
using BakeMyWorld.Website.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BakeMyWorld.Website.Areas.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class CorporatesController : ControllerBase
    {
        private readonly BakeMyWorldContext context;

        public CorporatesController(BakeMyWorldContext context)
        {
            this.context = context;
        }

      
        // GET: api/Corporates
        /// <summary>
        /// Provides the List of Corporates
        /// </summary>
        /// <returns>Categories</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Corporate>>> GetCorportes()
        {
            return await context.Corporates.ToListAsync();
        }

        // GET api/Corporates/5
        /// <summary>
        /// Provides a specific Corporate based on id number
        /// </summary>
        /// <returns>Category</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Corporate>> GetCorporte(int id)
        {
            var corporte = await context.Corporates.FindAsync(id);

            if (corporte == null)
            {
                return NotFound();
            }
            return corporte;
        }

        // POST api/Corporates
        /// <summary>
        /// Registers new Corporate
        /// </summary>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<Corporate>> PostCorporte(CorporateDto corporateDto)
        {
            var corporte = new Corporate(
                corporateDto.Id,
                corporateDto.Name,
                corporateDto.ImageUrl);

            context.Corporates.Add(corporte);

            await context.SaveChangesAsync();

            return CreatedAtAction("GetCorporte", new { id = corporte.Id }, corporte);

        }

        // Replace api/Corporates/5
        /// <summary>
        /// Updates a specific Corporate based on id number
        /// </summary>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCorporte(int id, CorporateDto corporateDto)
        {
            if (id != corporateDto.Id)
            {
                return BadRequest();
            }

            var corporate = new Corporate(
                corporateDto.Id,
                corporateDto.Name,
                corporateDto.ImageUrl);

            context.Entry(corporate).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CorporateExists(id))
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

        // DELETE api/Corporates/5
        /// <summary>
        /// Deletes a specific Corporate based on id number
        /// </summary>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCorporate(int id)
        {
            var corporate = await context.Corporates.FindAsync(id);

           
            if (corporate == null)
            {
                return NotFound();
            }

            context.Corporates.Remove(corporate);
           
            await context.SaveChangesAsync();

            return NoContent();
        }
          private bool CorporateExists(int id)
        {
            return context.Corporates.Any(c => c.Id == id);
        }
    }
}
