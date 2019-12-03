using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Entities;

namespace DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<Expense> Expenses { get; set; }
    }
}
