using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    /// <summary>
    /// Expense entity with fields:
    /// - Date
    /// - AmountOfMoney
    /// - CategoryId
    /// - UserId
    /// </summary>
    public class Expense
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double AmountOfMoney { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
