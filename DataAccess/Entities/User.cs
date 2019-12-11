using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Entities;

namespace DataAccess.Entities
{
    /// <summary>
    /// User entity with fields:
    /// - Name
    /// - Surname
    /// - Email
    /// - Password
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<Expense> Expenses { get; set; }
        public List<Income> Incomes { get; set; }
        public List<Dream> Dreams { get; set; }
    }
}
