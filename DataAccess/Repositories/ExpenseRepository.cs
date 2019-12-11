using System;
using System.Collections.Generic;
using System.Text;

using DataAccess.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Repository for Expenses table
    /// </summary>
    public class ExpenseRepository
    {
        private readonly MonnyDbContext dbContext;
        public ExpenseRepository()
        {
            dbContext = new MonnyDbContext();
        }
        public List<Expense> GetItems()
        {
            return dbContext.Set<Expense>().ToList();
        }
        public Expense GetItem(int id)
        {
            return dbContext.Set<Expense>().Find(id);
        }
        public void Create(Expense item)
        {
            dbContext.Set<Expense>().Add(item);
            dbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            Expense item = dbContext.Set<Expense>().Find(id);
            dbContext.Set<Expense>().Remove(item);
            dbContext.SaveChanges();
        }
        public void Update(Expense item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
