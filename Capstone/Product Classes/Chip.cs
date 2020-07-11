using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Chip : Product
    {
        public Chip(string productName, decimal price) : base(productName, price)
        {
        }
        public override string DispenseMessage()
        {
            return "Crunch Crunch, Yum!";
        }
    }
}
