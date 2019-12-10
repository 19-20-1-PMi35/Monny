using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class Income
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double MoneyCount { get; set; }
        public int? CategoryCheck { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
