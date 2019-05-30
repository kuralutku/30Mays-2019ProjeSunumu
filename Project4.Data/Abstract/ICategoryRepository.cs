using Project4.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project4.Data.Abstract
{
    public interface ICategoryRepository
    {
        Category GetById(int categoryId);
        IQueryable<Category> GetAll();
        void AddCategory(Category entity);
        void UpdateCategory(Category entity);
        void SaveCategory(Category entity);
        void DeleteCategory(int categoryId);
    }
}
