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
    public class RequestsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RequestsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequest()
        {
            return await _context.Request.ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Request.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        //GET: api/list-review/{id}
        [HttpGet("list-review/{id}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestsByStatus(int id) {
            var status = _context.Request.Where(s => s.Status == "Review" && s.Id != id).ToListAsync();
            return await status;
        }

        //api/Approve
        [HttpPut("approve")]
        public async Task<IActionResult> RequestApprove(Request request) {
            request.Status = "Approved";
            return await PutRequest(request.Id, request);
        }

        //api/Reject
        [HttpPut("reject")]
        public async Task<IActionResult> RequestRejected(Request request) {
            request.Status = "Rejected";
            return await PutRequest(request.Id, request);
        }

        //Put {/submit review}
        [HttpPut("/submit-review")]
        public async Task<IActionResult> SubmitReview(int id, Request request) {
            request.Status = request.Total <= 50 ? "Approved" : "Review";
            return await PutRequest(id, request);

        }

        // PUT: api/Requests/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/Requests
        
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            _context.Request.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Request>> DeleteRequest(int id)
        {
            var request = await _context.Request.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Request.Remove(request);
            await _context.SaveChangesAsync();

            return request;
        }

        private bool RequestExists(int id)
        {
            return _context.Request.Any(e => e.Id == id);
        }
    }
}
