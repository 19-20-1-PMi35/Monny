using System;
using System.Collections.Generic;
using System.Text;

using DataAccess.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Repository for Users table
    /// </summary>
    public class UserRepository
    {
        private readonly MonnyDbContext dbContext;
        public UserRepository()
        {
            dbContext = new MonnyDbContext();
        }
        public List<User> GetItems()
        {
            return dbContext.Set<User>().ToList();
        }
        public User GetItem(int id)
        {
            return dbContext.Set<User>().Find(id);
        }
        public void Create(User item)
        {
            dbContext.Set<User>().Add(item);
            dbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            User item = dbContext.Set<User>().Find(id);
            dbContext.Set<User>().Remove(item);
            dbContext.SaveChanges();
        }
        public void Update(User item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
