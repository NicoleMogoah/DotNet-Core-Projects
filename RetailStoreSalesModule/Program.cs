using System;
using System.Collections.Generic;

namespace RetailStoreSalesModule
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== RETAIL STORE SALES PERFORMANCE MODULE ===");
            Console.WriteLine("Awarding staff based on monthly sales performance\n");

            List<StaffMember> staffMembers = new List<StaffMember>();

            // Using do-while loop for multiple entries as required
            bool continueEntering = true;

            do
            {
                try
                {
                    // Capture staff member data
                    StaffMember staff = CaptureStaffData();
                    staffMembers.Add(staff);

                    // Ask if user wants to continue (do-while loop as required)
                    Console.Write("\nDo you want to enter another staff member? (yes/no): ");
                    string response = Console.ReadLine().ToLower();

                    continueEntering = (response == "yes" || response == "y");
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please try again.\n");
                }

            } while (continueEntering);

            // Display all staff members and their awards
            DisplayAllStaffMembers(staffMembers);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        // Method to capture data for each staff member
        static StaffMember CaptureStaffData()
        {
            Console.WriteLine("--- Enter Staff Member Details ---");

            // Get staff name
            Console.Write("Enter staff member name: ");
            string name = Console.ReadLine();

            // Get product category
            string category = GetProductCategory();

            // Get monthly sales amount with validation
            decimal salesAmount = GetMonthlySalesAmount();

            // Assign award based on sales amount
            string award = AssignAward(salesAmount);

            // Create and return staff object
            return new StaffMember(name, category, salesAmount, award);
        }

        // Method to get product category (FIXED VERSION)
        static string GetProductCategory()
        {
            Console.WriteLine("\nSelect Product Category:");
            Console.WriteLine("1. Electronics");
            Console.WriteLine("2. Clothing");
            Console.WriteLine("3. Home Appliances");
            Console.WriteLine("4. Beauty Products");
            Console.WriteLine("5. Groceries");
            Console.WriteLine("6. Other");

            Console.Write("Enter choice (1-6): ");
            string choice = Console.ReadLine();

            // Using traditional switch statement instead of switch expression
            switch (choice)
            {
                case "1":
                    return "Electronics";
                case "2":
                    return "Clothing";
                case "3":
                    return "Home Appliances";
                case "4":
                    return "Beauty Products";
                case "5":
                    return "Groceries";
                case "6":
                    return "Other";
                default:
                    return "Unknown Category";
            }
        }

        // Method to get monthly sales amount with validation
        static decimal GetMonthlySalesAmount()
        {
            decimal salesAmount = 0;
            bool validInput = false;

            while (!validInput)
            {
                Console.Write("Enter total monthly sales amount (KES): ");
                string input = Console.ReadLine();

                if (decimal.TryParse(input, out salesAmount))
                {
                    if (salesAmount >= 0)
                    {
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

            return salesAmount;
        }

        // Method to assign award based on monthly sales
        static string AssignAward(decimal salesAmount)
        {
            if (salesAmount >= 400000)
                return "Platinum Seller Award";
            else if (salesAmount >= 300000)
                return "Gold Seller Award";
            else if (salesAmount >= 200000)
                return "Silver Seller Award";
            else if (salesAmount >= 100000)
                return "Bronze Seller Award";
            else
                return "Participant Award";
        }

        // Method to display all staff members and their awards
        static void DisplayAllStaffMembers(List<StaffMember> staffMembers)
        {
            Console.WriteLine("\n" + new string('=', 80));
            Console.WriteLine("STAFF PERFORMANCE AWARDS SUMMARY");
            Console.WriteLine(new string('=', 80));
            Console.WriteLine("{0,-20} {1,-20} {2,-20} {3,-25}",
                "Staff Name", "Category", "Sales (KES)", "Award");
            Console.WriteLine(new string('-', 80));

            foreach (var staff in staffMembers)
            {
                staff.DisplayInfo();
            }

            Console.WriteLine(new string('=', 80));
            Console.WriteLine($"Total Staff Members Registered: {staffMembers.Count}");
        }
    }

    // StaffMember class to store staff data
    class StaffMember
    {
        public string Name { get; set; }
        public string ProductCategory { get; set; }
        public decimal MonthlySales { get; set; }
        public string Award { get; set; }

        public StaffMember(string name, string category, decimal sales, string award)
        {
            Name = name;
            ProductCategory = category;
            MonthlySales = sales;
            Award = award;
        }

        public void DisplayInfo()
        {
            Console.WriteLine("{0,-20} {1,-20} {2,-20} {3,-25}",
                Name, ProductCategory, MonthlySales.ToString("N0"), Award);
        }
    }
}