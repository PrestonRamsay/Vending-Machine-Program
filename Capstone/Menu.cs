using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    //Display used here as in "look at this display" not "display this menu"
    public class Menu
    {   
        public void DisplayMenu(string[] options)
        {
            DisplayMenu(options, 0);
        }
        public void DisplayMenu(string[] options, int numberOfHiddenItems)
        {
            Console.WriteLine("Choose an option by entering the corresponding number\n");

            for (int i = 0; i < options.Length - numberOfHiddenItems; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }
        }
    }
}
