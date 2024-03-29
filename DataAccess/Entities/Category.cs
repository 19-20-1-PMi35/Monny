﻿using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Entities;

namespace DataAccess.Entities
{
    /// <summary>
    /// Category entity with fields:
    /// - Id
    /// - Name
    /// - CategoryType
    /// </summary>
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CategoryType { get; set; }
        public List<Expense> Expenses { get; set; }
        public List<Income> Incomes { get; set; }
    }
}
