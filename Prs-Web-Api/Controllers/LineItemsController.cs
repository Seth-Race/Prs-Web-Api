using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prs_Web_Api.Data;
using Prs_Web_Api.Models;

namespace Prs_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LineItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/LineItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LineItem>>> GetLineItem()
        {
            return await _context.LineItem.Include(x => x.Product).
                Include(y => y.Request).ToListAsync();
        }

        // GET: api/LineItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LineItem>> GetLineItem(int id)
        {
            var lineItem = await _context.LineItem.FindAsync(id);

            if (lineItem == null)
            {
                return NotFound();
            }

            return lineItem;
        }

        // PUT: api/LineItems/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLineItem(int id, LineItem lineItem)
        {
            if (id != lineItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(lineItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LineItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            await RecalculateTotal(lineItem.RequestId);
            return NoContent();
        }

        // POST: api/LineItems
        
        [HttpPost]
        public async Task<ActionResult<LineItem>> PostLineItem(LineItem lineItem)
        {
            _context.LineItem.Add(lineItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLineItem", new { id = lineItem.Id }, lineItem);
        }

        // DELETE: api/LineItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LineItem>> DeleteLineItem(int id)
        {
            var lineItem = await _context.LineItem.FindAsync(id);
            if (lineItem == null)
            {
                return NotFound();
            }

            _context.LineItem.Remove(lineItem);
            await _context.SaveChangesAsync();

            return lineItem;
        }

        private bool LineItemExists(int id)
        {
            return _context.LineItem.Any(e => e.Id == id);
        }

        public async Task RecalculateTotal(int requestID) {
            var request = await _context.Request.FindAsync(requestID);
            request.Total = (from l in _context.LineItem
                             join p in _context.Product on l.ProductId equals p.Id
                             where l.RequestId == requestID
                             select new { Total = l.Quantity * p.Price })
                             .Sum(x => x.Total);
            var rc = await _context.SaveChangesAsync();
            if (rc != 1) throw new Exception("Fatal Error: Did not calculate.");

        }

    }
}
