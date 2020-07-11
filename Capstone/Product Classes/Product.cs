using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public abstract class Product
    {
        public Product(string productName, decimal price)
        {            
            Name = productName;
            PurchasePrice = price;
            Quantity = 5;
            AmountSold = 0;
        }
        public string Name { get; }
        public decimal PurchasePrice { get; }
        public int Quantity { get; set; }
        public int AmountSold { get; set; }
        public abstract string DispenseMessage();        
    }
}
