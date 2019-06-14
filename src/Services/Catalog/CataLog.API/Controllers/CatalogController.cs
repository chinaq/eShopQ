using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopOnContainers.Services.Catalog.API.Infrastructure;
using Microsoft.eShopOnContainers.Services.Catalog.API.Model;

namespace CataLog.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogContext _context;

        public CatalogController(CatalogContext context)
        {
            _context = context;
        }

        // GET: api/Catalog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatalogItem>>> GetCatalogItems()
        {
            return await _context.CatalogItems.ToListAsync();
        }

        // GET: api/Catalog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogItem>> GetCatalogItem(int id)
        {
            var catalogItem = await _context.CatalogItems.FindAsync(id);

            if (catalogItem == null)
            {
                return NotFound();
            }

            return catalogItem;
        }

        // PUT: api/Catalog/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatalogItem(int id, CatalogItem catalogItem)
        {
            if (id != catalogItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(catalogItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatalogItemExists(id))
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

        // POST: api/Catalog
        [HttpPost]
        public async Task<ActionResult<CatalogItem>> PostCatalogItem(CatalogItem catalogItem)
        {
            _context.CatalogItems.Add(catalogItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCatalogItem", new { id = catalogItem.Id }, catalogItem);
        }

        // DELETE: api/Catalog/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CatalogItem>> DeleteCatalogItem(int id)
        {
            var catalogItem = await _context.CatalogItems.FindAsync(id);
            if (catalogItem == null)
            {
                return NotFound();
            }

            _context.CatalogItems.Remove(catalogItem);
            await _context.SaveChangesAsync();

            return catalogItem;
        }

        private bool CatalogItemExists(int id)
        {
            return _context.CatalogItems.Any(e => e.Id == id);
        }
    }
}
