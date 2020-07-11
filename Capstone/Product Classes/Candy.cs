using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Candy : Product
    {
        public Candy(string productName, decimal price) : base(productName, price)
        {
        }
        public override string DispenseMessage()
        {
            return "Munch Munch, Yum!";
        }
    }
}
