using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Gum : Product
    {
        public Gum(string productName, decimal price) : base(productName, price)
        {
        }
        public override string DispenseMessage()
        {
            return "Chew Chew, Yum!";
        }
    }
}
