using System;
using System.Collections.Generic;

namespace RetailSalesPerformance
{
    class Program
    {
        // Function to compute total sales
        static decimal ComputeTotalSales(decimal[] sales)
        {
            decimal total = 0;
            foreach (decimal sale in sales)
            {
                total += sale;
            }
            return total;
        }

        // Function to compute average sales
        static decimal ComputeAverageSales(decimal[] sales)
        {
            decimal total = ComputeTotalSales(sales);
            return total / sales.Length;
        }

        // Function to assign performance rating based on total sales
        static string AssignPerformanceRating(decimal totalSales)
        {
            if (totalSales >= 500000)
                return "Outstanding";
            else if (totalSales >= 300000)
                return "Excellent";
            else if (totalSales >= 150000)
                return "Good";
            else
                return "Needs Improvement";
        }

        // Function to validate sales amount (must be 0 or positive)
        static bool ValidateSalesAmount(decimal amount)
        {
            return amount >= 0;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("=== RETAIL STORE SALES PERFORMANCE SYSTEM ===");
            Console.WriteLine("Product Categories: Electronics, Clothing, Home Appliances, Beauty Products, Groceries\n");

            // Array to store product categories as specified in requirements
            string[] productCategories = {
                "Electronics",
                "Clothing",
                "Home Appliances",
                "Beauty Products",
                "Groceries"
            };

            // List to store salesperson data
            List<Salesperson> salespersons = new List<Salesperson>();

            try
            {
                // Input data for 5 salespersons as required
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"\n--- Enter details for Salesperson {i + 1} ---");

                    // Get salesperson name
                    Console.Write("Enter salesperson name: ");
                    string name = Console.ReadLine();

                    // Array to store sales for each category
                    decimal[] sales = new decimal[5];

                    // Get sales amounts for each product category
                    for (int j = 0; j < productCategories.Length; j++)
                    {
                        bool validInput = false;
                        while (!validInput)
                        {
                            Console.Write($"Enter sales amount for {productCategories[j]} (KES): ");
                            string input = Console.ReadLine();

                            if (decimal.TryParse(input, out decimal amount))
                            {
                                if (ValidateSalesAmount(amount))
                                {
                                    sales[j] = amount;
                                    validInput = true;
                                }
                                else
                                {
                                    Console.WriteLine("Error: Sales amount cannot be negative. Please try again.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error: Please enter a valid number.");
                            }
                        }
                    }

                    // Calculate performance metrics using our functions
                    decimal totalSales = ComputeTotalSales(sales);
                    decimal averageSales = ComputeAverageSales(sales);
                    string rating = AssignPerformanceRating(totalSales);

                    // Create salesperson object and add to list
                    Salesperson salesperson = new Salesperson(name, sales, totalSales, averageSales, rating);
                    salespersons.Add(salesperson);

                    Console.WriteLine($"Data recorded for {name}");
                }

                // Display comprehensive results
                DisplaySalesReport(salespersons);

                // Test with 3 different profiles as required
                Console.WriteLine("\n" + new string('=', 100));
                Console.WriteLine("TEST CASES WITH 3 DIFFERENT SALESPERSON PROFILES");
                Console.WriteLine(new string('=', 100));
                Console.WriteLine("{0,-30} {1,-20} {2,-20} {3,-25}",
                    "Salesperson Profile", "Total Sales (KES)", "Average Sales (KES)", "Performance Rating");
                Console.WriteLine(new string('-', 100));

                // Test case 1: High performer
                TestSalesperson("John Doe (High Performer)", new decimal[] { 150000, 120000, 100000, 80000, 70000 });

                // Test case 2: Average performer  
                TestSalesperson("Jane Smith (Average Performer)", new decimal[] { 80000, 70000, 60000, 40000, 30000 });

                // Test case 3: Low performer
                TestSalesperson("Mike Johnson (Low Performer)", new decimal[] { 40000, 30000, 20000, 10000, 5000 });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Console.WriteLine("Please restart the program and try again.");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        // Method to display sales performance report
        static void DisplaySalesReport(List<Salesperson> salespersons)
        {
            Console.WriteLine("\n" + new string('=', 90));
            Console.WriteLine("SALES PERFORMANCE SUMMARY REPORT");
            Console.WriteLine(new string('=', 90));
            Console.WriteLine("{0,-20} {1,-20} {2,-20} {3,-25}",
                "Salesperson Name", "Total Sales (KES)", "Average Sales (KES)", "Performance Rating");
            Console.WriteLine(new string('-', 90));

            foreach (var person in salespersons)
            {
                person.DisplayPerformance();
            }

            Console.WriteLine(new string('=', 90));
        }

        // Method to test individual salesperson profiles
        static void TestSalesperson(string name, decimal[] sales)
        {
            decimal total = ComputeTotalSales(sales);
            decimal average = ComputeAverageSales(sales);
            string rating = AssignPerformanceRating(total);

            Console.WriteLine("{0,-30} {1,-20} {2,-20} {3,-25}",
                name, total.ToString("N0"), average.ToString("N0"), rating);
        }
    }

    // Salesperson class to store and manage sales data
    class Salesperson
    {
        public string Name { get; set; }
        public decimal[] Sales { get; set; }
        public decimal TotalSales { get; set; }
        public decimal AverageSales { get; set; }
        public string PerformanceRating { get; set; }

        // Constructor to initialize salesperson data
        public Salesperson(string name, decimal[] sales, decimal totalSales, decimal averageSales, string rating)
        {
            Name = name;
            Sales = sales;
            TotalSales = totalSales;
            AverageSales = averageSales;
            PerformanceRating = rating;
        }

        // Method to display salesperson performance
        public void DisplayPerformance()
        {
            Console.WriteLine("{0,-20} {1,-20} {2,-20} {3,-25}",
                Name, TotalSales.ToString("N0"), AverageSales.ToString("N0"), PerformanceRating);
        }
    }
}