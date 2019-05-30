﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project4.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project4.Data.Concrete.EfCore
{
    public static class SeedData
    {
        public static void Seed(IApplicationBuilder app)
        {
            BlogContext context = app.ApplicationServices.GetRequiredService<BlogContext>();

            context.Database.Migrate();

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category() { Name = "Category 1" },
                    new Category() { Name = "Category 2" },
                    new Category() { Name = "Category 3" }
                    );

                context.SaveChanges();
            }

            if (!context.Blogs.Any())
            {
                context.Blogs.AddRange(

                        new Blog() {Title="Blog title 1", Descriptin="Blog Description", Body="Blog Body 1", Image="1.jpg", Date=DateTime.Now.AddDays(-5), isApproved=true, CategoryId=1},
                        new Blog() {Title="Blog title 2", Descriptin="Blog Description", Body="Blog Body 2", Image="2.jpg", Date=DateTime.Now.AddDays(-7), isApproved=true, CategoryId=1},
                        new Blog() {Title="Blog title 3", Descriptin="Blog Description", Body="Blog Body 3", Image="3.jpg", Date=DateTime.Now.AddDays(-8), isApproved=false, CategoryId=2},
                        new Blog() {Title="Blog title 4", Descriptin="Blog Description", Body="Blog Body 4", Image="4.jpg", Date=DateTime.Now.AddDays(-9), isApproved=true, CategoryId=3}

                    );
                context.SaveChanges();
            }
        }
    }
}
