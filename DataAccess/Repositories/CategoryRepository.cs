using System;
using System.Collections.Generic;
using System.Text;

using DataAccess.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Repository for Categories table
    /// </summary>
    public class CategoryRepository
    {
        private readonly MonnyDbContext dbContext;
        public CategoryRepository()
        {
            dbContext = new MonnyDbContext();
        }
        public List<Category> GetItems()
        {
            return dbContext.Set<Category>().ToList();
        }
        public Category GetItem(int id)
        {
            return dbContext.Set<Category>().Find(id);
        }
        public void Create(Category item)
        {
            dbContext.Set<Category>().Add(item);
            dbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            Category item = dbContext.Set<Category>().Find(id);
            dbContext.Set<Category>().Remove(item);
            dbContext.SaveChanges();
        }
        public void Update(Category item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
