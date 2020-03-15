using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api1.Api1Model.Models;
using Api1.Api1Model.Data;
using Common.CommonLogic.Logics;

namespace Api1.Api1Logic.Logics
{
    public class BlogLogic
    {
        private readonly Api1Context _context;

        public BlogLogic(Api1Context context)
        {
            _context = context;
        }

        public async Task<List<Blog>> GetBlog()
        {
            var blogs = _context.Blog
                .Include(blog => blog.Posts);
            return await blogs.ToListAsync();
        }

        public async Task<Blog> GetBlog(int id)
        {
            var blog = _context.Blog
                .Include(blog => blog.Posts);

            return await blog.FirstOrDefaultAsync(blog => blog.BlogId == id);
        }

        public async Task PutBlog(Blog blog, DateTime tsStamp, string usStamp, string asStamp)
        {
            PostLogic postLogic = new PostLogic(_context);

            blog.SetBaseData(tsStamp, usStamp, asStamp);
            _context.Entry(blog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await postLogic.PutPosts(blog.Posts, tsStamp, usStamp, asStamp);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task PostBlog(Blog blog, DateTime tsStamp, string usStamp, string asStamp)
        {
            PostLogic postLogic = new PostLogic(_context);

            blog.SetBaseData(tsStamp, usStamp, asStamp);
            _context.Entry(blog).State = EntityState.Added;

            await _context.SaveChangesAsync();
            await postLogic.PostPosts(blog.Posts, tsStamp, usStamp, asStamp);
        }

        public async Task<Blog> DeleteBlog(int id)
        {
            var blog = await _context.Blog.FindAsync(id);

            _context.Blog.Remove(blog);
            await _context.SaveChangesAsync();

            return blog;
        }

        public bool BlogExists(int id)
        {
            return _context.Blog.Any(e => e.BlogId == id);
        }
    }
}
