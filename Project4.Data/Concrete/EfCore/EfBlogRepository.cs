using Project4.Data.Abstract;
using Project4.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project4.Data.Concrete.EfCore
{
    public class EfBlogRepository : IBlogRepository
    {

        private BlogContext context;

        public EfBlogRepository(BlogContext _context)
        {
            context = _context;
        }

        public void AddBlog(Blog entity)
        {
            context.Blogs.Add(entity);
            context.SaveChanges();
        }

        public void DeleteBlog(int blogId)
        {
            var blog = context.Blogs.FirstOrDefault(p => p.BlogId == blogId);
            if (blog != null)
            {
                context.Blogs.Remove(blog);
                context.SaveChanges();
            }
        }

        public IQueryable<Blog> GetAll()
        {
            return context.Blogs;
        }

        public Blog GetById(int blogId)
        {
            return context.Blogs.FirstOrDefault(p => p.BlogId == blogId);
        }

        public void SaveBlog(Blog entity)
        {
            if (entity.BlogId == 0)
            {
                entity.Date = DateTime.Now;
                context.Blogs.Add(entity);
            }
            else
            {
                var blog = GetById(entity.BlogId);

                if (blog != null)
                {
                    blog.Title = entity.Title;
                    blog.Descriptin = entity.Descriptin;
                    blog.Body = entity.Body;
                    blog.CategoryId = entity.CategoryId;
                    blog.Image = entity.Image;
                    blog.isHome = entity.isHome;
                    blog.isApproved = entity.isApproved;
                    blog.isSlider = entity.isSlider;
                }
            }

            context.SaveChanges();
        }

        public void UpdateBlog(Blog entity)
        {
            var blog = GetById(entity.BlogId);

            if (blog != null)
            {
                blog.Title = entity.Title;
                blog.Descriptin = entity.Descriptin;
                blog.CategoryId = entity.CategoryId;
                blog.Image = entity.Image;

                context.SaveChanges();
            }

            context.SaveChanges();
        }
    }
}
