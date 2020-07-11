using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Dynamic;

namespace Capstone
{
    public class VendingMachine
    {
        public Dictionary<string, Product> AllProducts = new Dictionary<string, Product>();
        public decimal StoredMoney { get; set; }
        public VendingMachine(string filePath)
        {
            StoredMoney = 0.00M;
            bool fileExists = File.Exists(filePath);

            if (fileExists)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        //the input file fills our AllProducts dictionary
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();

                            string[] productProperties = line.Split("|");
                            string slotLocation = productProperties[0];
                            string productName = productProperties[1];
                            decimal price = decimal.Parse(productProperties[2]);
                            Product currentProduct = null;

                            if (productProperties[3].ToLower() == "candy")
                            {
                                currentProduct = new Candy(productName, price);
                            }
                            if (productProperties[3].ToLower() == "chip")
                            {
                                currentProduct = new Chip(productName, price);
                            }
                            if (productProperties[3].ToLower() == "drink")
                            {
                                currentProduct = new Drink(productName, price);
                            }
                            if (productProperties[3].ToLower() == "gum")
                            {
                                currentProduct = new Gum(productName, price);
                            }
                            AllProducts.Add(slotLocation, currentProduct);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("There was a problem running reading the file");
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
        }
        public string DisplayProducts()
        {
            string returnString = "";

            foreach (string key in AllProducts.Keys)
            {
                if (AllProducts[key].Quantity == 0)
                {
                    returnString += $"{key} {AllProducts[key].Name} ${AllProducts[key].PurchasePrice} SOLD OUT\n";
                }
                else
                {
                    returnString += $"{key} {AllProducts[key].Name} ${AllProducts[key].PurchasePrice} Remaining: {AllProducts[key].Quantity}\n";
                }
            }

            return returnString;
        }
        public string FeedMoney(string userInput)
        {
            //Machine only accepts 1s, 2s, 5s, or 10s
            string returnString = $"\nInsert a bill: $(1), $(2), $(5), or $(10)";

            decimal moneyInserted = decimal.Parse(userInput);
            bool successfulFeed = false;

            if (moneyInserted == 1 || moneyInserted == 2 || moneyInserted == 5 || moneyInserted == 10)
            {
                StoredMoney += moneyInserted;
                successfulFeed = true;
            }
            else
            {
                returnString += "\nMoney insert value not accepted, try again.";
            }

            if (successfulFeed)
            {
                Logger.FeedMoney(moneyInserted, StoredMoney);             
            }

            return returnString;
        }
        public string SelectItemAndPurchase(string userSelectedSlot)
        {
            bool isInvalidSlot = true;
            string returnString = "";
            Product selectedProduct = AllProducts[userSelectedSlot];
            decimal storedMoneyForLog = StoredMoney;

            if (selectedProduct.Quantity == 0)
            {
                returnString = "Product is SOLD OUT. Select a different item or exit.";
            }
            else if (StoredMoney >= selectedProduct.PurchasePrice)
            {
                decimal newMoney = StoredMoney - selectedProduct.PurchasePrice;           
                StoredMoney = newMoney;
                returnString = $"You bought {selectedProduct.Name} for ${selectedProduct.PurchasePrice} {selectedProduct.DispenseMessage()}";
                selectedProduct.Quantity--;
                selectedProduct.AmountSold++;
            }
            else if (StoredMoney < selectedProduct.PurchasePrice)
            {
                returnString = "Insufficient funds. Insert more money or select a different item.";
            }
            else if (isInvalidSlot)
            {
                returnString = "Vending slot invalid, returning to purchase menu.";
            }

            Logger.Purchase(userSelectedSlot, selectedProduct, storedMoneyForLog);

            return returnString;
        }
        public int[] GiveChange()
        {
            decimal quarter = 0.25M;
            decimal dime = 0.10M;
            decimal nickel = 0.05M;
            int amountOfQuarters = 0;
            int amountOfDimes = 0;
            int amountOfNickels = 0;
            decimal storedMoneyForLog = StoredMoney;

            if (StoredMoney >= quarter)
            {
                amountOfQuarters = (int)Math.Floor(StoredMoney / quarter);
                StoredMoney -= amountOfQuarters * quarter;
            }
            if (StoredMoney >= dime)
            {
                amountOfDimes = (int)Math.Floor(StoredMoney / dime);
                StoredMoney -= amountOfDimes * dime;
            }
            if (StoredMoney >= nickel)
            {
                amountOfNickels = (int)Math.Floor(StoredMoney / nickel);
                StoredMoney -= amountOfNickels * nickel;
            }

            int[] changeInCoins = { amountOfQuarters, amountOfDimes, amountOfNickels };
            StoredMoney = 0.00M;

            Logger.GiveChange(storedMoneyForLog);

            return changeInCoins;
        }
    }
}
