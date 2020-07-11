using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Capstone
{
    //Made class Public
    public class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string inputFileName = "vendingmachine.csv";           
            string readFilePath = Path.Combine(currentDirectory, inputFileName);            

            VendingMachine vendoMatic600 = new VendingMachine(readFilePath);
            bool exitMainMenu = false;
            Menu mainMenu = new Menu();
            string[] mainMenuOptions = { "Display Items", "Purchase", "EXIT", "Sales Report" };
            Menu purchaseMenu = new Menu();
            string[] purchaseMenuOptions = { "Feed Money", "Select Product", "Finish Transaction" };            

            //while loop returns user to the main menu
            while (!exitMainMenu)
            {
                mainMenu.DisplayMenu(mainMenuOptions, 1);
                string inputKey = Console.ReadLine();                

                if (inputKey == "1")
                {
                    Console.Clear();
                    Console.WriteLine(vendoMatic600.DisplayProducts());
                }
                else if (inputKey == "2")
                {
                    //purchaseMenu is displayed, loop for same reason
                    bool exitPurchaseMenu = false;
                    while (!exitPurchaseMenu)
                    {
                        Console.WriteLine();
                        purchaseMenu.DisplayMenu(purchaseMenuOptions);                        
                        string purchaseInputKey = Console.ReadLine();
                        Console.WriteLine($"\nCurrent Money: {vendoMatic600.StoredMoney.ToString("C2")}");
                      
                        if (purchaseInputKey == "1")
                        {
                            Console.Clear();
                            Console.WriteLine($"\nInsert a bill: $(1), $(2), $(5), or $(10)");
                            string moneyInserted = Console.ReadLine();
                            Console.WriteLine(vendoMatic600.FeedMoney(moneyInserted));                                                                                  
                        }
                        else if (purchaseInputKey == "2")
                        {
                            Console.Clear();
                            Console.WriteLine(vendoMatic600.DisplayProducts());
                            Console.WriteLine($"Current Money: {vendoMatic600.StoredMoney.ToString("C2")}");
                            Console.WriteLine("\nSelect the product you would like to purchase by entering its slot. Ex: A4, C3, B1, D2, etc");
                            string userSelectedSlot = Console.ReadLine().ToUpper();
                            Console.Clear();
                            Console.WriteLine(vendoMatic600.SelectItemAndPurchase(userSelectedSlot));
                        }
                        else if (purchaseInputKey == "3")
                        {
                            int[] finalChangeInCoins = vendoMatic600.GiveChange();

                            Console.WriteLine($"\nYour change is: {finalChangeInCoins[0]} quarter(s), {finalChangeInCoins[1]} dime(s), {finalChangeInCoins[2]} nickel(s)");
                            exitPurchaseMenu = true;
                        }
                        else
                        {
                            Console.WriteLine("\nUnknown input. Try again.");
                        }
                    }
                }
                else if (inputKey == "3")
                {
                    if (vendoMatic600.StoredMoney != 0)
                    {
                        vendoMatic600.GiveChange();
                    }
                    Console.WriteLine();
                    exitMainMenu = true;
                }
                else if (inputKey == "4")
                {
                    SalesReport salesReport = new SalesReport();

                    string partialFileName = "SalesReport";
                    DirectoryInfo salesReportDirectory = new DirectoryInfo(currentDirectory);
                    FileInfo[] salesReportFiles = salesReportDirectory.GetFiles(partialFileName + "*.txt");

                    if (salesReportFiles.Length == 0)
                    {
                        salesReport.CreateFirstReport(vendoMatic600);
                    }
                    else
                    {
                        salesReport.CreateUpdatedSalesReport(vendoMatic600);
                    }
                    Console.WriteLine("\nSales Report Generated\n");
                }
                else
                {
                    Console.WriteLine("\nUnknown input. Try again.\n");
                }
            }
        }
    }
}
