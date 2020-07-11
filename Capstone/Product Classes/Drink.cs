using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Drink : Product
    {
        public Drink(string productName, decimal price) : base(productName, price)
        {
        }
        public override string DispenseMessage()
        {
            return "Glug Glug, Yum!";
        }
    }
}
