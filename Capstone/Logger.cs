using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone
{
    public static class Logger
    {
        static string currentDirectory = Directory.GetCurrentDirectory();
        static string auditLog = "Log.txt";
        static string logFilePath = Path.Combine(currentDirectory, auditLog);
        public static void FeedMoney(decimal moneyInserted, decimal storedMoney)
        {
            using (StreamWriter sw = new StreamWriter(logFilePath, true))
            {
                sw.WriteLine($"{DateTime.Now} FEED MONEY: {moneyInserted.ToString("C2")} {storedMoney.ToString("C2")}");
            }
        }
        public static void Purchase(string userSelectedSlot, Product selectedProduct, decimal storedMoney)
        {            
            decimal newMoney = storedMoney - selectedProduct.PurchasePrice;

            using (StreamWriter sw = new StreamWriter(logFilePath, true))
            {
                sw.WriteLine($"{DateTime.Now} {selectedProduct.Name} {userSelectedSlot} {storedMoney.ToString("C2")} {newMoney.ToString("C2")}");
            }
        }
        public static void GiveChange(decimal storedMoney)
        {
            using (StreamWriter sw = new StreamWriter(logFilePath, true))
            {
                sw.WriteLine($"{DateTime.Now} GIVE CHANGE: {storedMoney.ToString("C2")} $0.00");
            }
        }
    }
}
