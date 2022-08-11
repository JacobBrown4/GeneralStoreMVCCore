﻿using System;
using System.Collections.Generic;

namespace GeneralStoreMVC.Data
{
    public partial class Product
    {
        public Product()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int QuantityInStock { get; set; }
        public double Price { get; set; }
        public int ProductType { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
