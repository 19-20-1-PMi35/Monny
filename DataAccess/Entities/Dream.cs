using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class Dream
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
