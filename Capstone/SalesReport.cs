using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone
{
    public class SalesReport
    {       
        public void CreateFirstReport(VendingMachine vendoMatic600)
        {
            //formatting the timestamp for the file name
            string timeStamp = $"{DateTime.Now}";
            timeStamp = timeStamp.Replace("/", "-").Replace(":", ".");
            string firstSalesReport = $"SalesReport({timeStamp}).txt";
            string currentDirectory = Directory.GetCurrentDirectory();
            string pathToFirstSalesReport = Path.Combine(currentDirectory, firstSalesReport);

            decimal totalSales = 0;

            using (StreamWriter sw = new StreamWriter(pathToFirstSalesReport))
            {
                foreach (string key in vendoMatic600.AllProducts.Keys)
                {
                    sw.WriteLine($"{vendoMatic600.AllProducts[key].Name}|{vendoMatic600.AllProducts[key].AmountSold}");
                    totalSales += vendoMatic600.AllProducts[key].PurchasePrice * vendoMatic600.AllProducts[key].AmountSold;
                }
                sw.WriteLine($"\nTOTAL SALES: {totalSales.ToString("C2")}");
            }            
        }
        public void CreateUpdatedSalesReport(VendingMachine vendoMatic600)
        {
            //searching for files with "SalesReport" in the name and creating an array of what it finds
            string partialFileName = "SalesReport";
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo salesReportDirectory = new DirectoryInfo(currentDirectory);           
            FileInfo[] salesReportFiles = salesReportDirectory.GetFiles(partialFileName + "*.txt");

            //finds the most recent sales report
            DateTime mostRecentFileDate = new DateTime(1,1,1);
            foreach (FileInfo file in salesReportFiles)
            {
                DateTime fileDateTime = File.GetLastWriteTime(file.FullName);
                if (fileDateTime > mostRecentFileDate)
                {
                    mostRecentFileDate = fileDateTime;
                }
            }

            //formatting the timestamps for the file name
            string lastSalesReportFileDate = $"{mostRecentFileDate}";
            lastSalesReportFileDate = lastSalesReportFileDate.Replace("/", "-").Replace(":", ".");
            string timeStamp = $"{DateTime.Now}";
            timeStamp = timeStamp.Replace("/", "-").Replace(":", ".");

            //paths for the reading and writing of the sales reports
            string salesReportFile = $"SalesReport({lastSalesReportFileDate}).txt";
            string updatedSalesReportFile = $"SalesReport({timeStamp}).txt";
            string pathToUpdatedSalesReport = Path.Combine(currentDirectory, updatedSalesReportFile);

            try
            {                
                using (StreamReader sr = new StreamReader(salesReportFile))
                {
                    using (StreamWriter sw = new StreamWriter(pathToUpdatedSalesReport))
                    {
                        decimal totalSales = 0; // Should be old totalSales???
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            string[] productLine = line.Split("|"); // Not spliting old totalSales???
                            int amountSold = int.Parse(productLine[1]);                            

                            foreach (string key in vendoMatic600.AllProducts.Keys)
                            {                                
                                if (productLine[0] == vendoMatic600.AllProducts[key].Name)
                                {
                                    amountSold += vendoMatic600.AllProducts[key].AmountSold;
                                    totalSales += vendoMatic600.AllProducts[key].PurchasePrice * vendoMatic600.AllProducts[key].AmountSold;
                                    sw.WriteLine($"{vendoMatic600.AllProducts[key].Name}|{amountSold}");
                                }                                
                            }                            
                        }
                        sw.WriteLine($"\nTOTAL SALES: {totalSales.ToString("C2")}"); // Goal to have oldTotal + newTotal;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An unexpected error occurred, verify the updated sales report");
                Console.WriteLine(e.Message);
            }
        }
    }
}