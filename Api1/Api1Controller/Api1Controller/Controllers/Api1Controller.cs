using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api1.Api1Model.Models;
using Api1.Api1Model.Data;
using Api1.Api1Logic.Logics;

namespace Api1Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Api1Controller : ControllerBase
    {
        private readonly Api1Context _context;

        private readonly BlogLogic _logic;

        public Api1Controller(Api1Context context)
        {
            _context = context;
            _logic = new BlogLogic(context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlog()
        {
            return await _logic.GetBlog();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlog(int id)
        {
            var blog = await _logic.GetBlog(id);

            if (blog == null)
            {
                return NotFound();
            }

            return blog;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog(int id, Blog blog)
        {
            if (id != blog.BlogId)
            {
                return BadRequest();
            }

            DateTime tsStamp = DateTime.Now;
            string usStamp = "testUser";
            string asStamp = "testApp";

            await _logic.PutBlog(blog, tsStamp, usStamp, asStamp);

            try
            {
                _context.Entry(blog).State = EntityState.Modified;
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!_logic.BlogExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Blog>> PostBlog(Blog blog)
        {
            DateTime tsStamp = DateTime.Now;
            string usStamp = "testUser";
            string asStamp = "testApp";

            await _logic.PostBlog(blog, tsStamp, usStamp, asStamp);

            return CreatedAtAction(nameof(GetBlog), new { id = blog.BlogId }, blog);
        }

        // DELETE: api/Api1/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Blog>> DeleteBlog(int id)
        {
            if (!_logic.BlogExists(id))
            {
                return NotFound();
            }

            return await _logic.DeleteBlog(id);
        }
    }
}
