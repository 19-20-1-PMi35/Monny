using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Entities;

namespace DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public List<Expense> Expenses { get; set; }
    }
}
