using System;
using System.Collections.Generic;
using System.Text;

using DataAccess.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Repository for Incomes table
    /// </summary>
    public class IncomeRepository
    {
        private readonly MonnyDbContext dbContext;
        public IncomeRepository()
        {
            dbContext = new MonnyDbContext();
        }
        public List<Income> GetItems()
        {
            return dbContext.Set<Income>().ToList();
        }
        public Income GetItem(int id)
        {
            return dbContext.Set<Income>().Find(id);
        }
        public void Create(Income item)
        {
            dbContext.Set<Income>().Add(item);
            dbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            Income item = dbContext.Set<Income>().Find(id);
            dbContext.Set<Income>().Remove(item);
            dbContext.SaveChanges();
        }
        public void Update(Income item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
