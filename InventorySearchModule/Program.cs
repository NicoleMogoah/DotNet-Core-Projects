using System;

namespace InventorySearchModule
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== INVENTORY SEARCH MODULE ===");
            Console.WriteLine("Binary Search for Product IDs\n");

            // Store product IDs in array as required
            int[] productIDs = { 1023, 1145, 2040, 2750, 3189, 3650, 4021, 4578, 4900, 5120 };

            try
            {
                // Display available product IDs
                DisplayProductIDs(productIDs);

                // Get product ID to search from user
                int searchID = GetProductIDFromUser();

                // Perform binary search
                int position = BinarySearch(productIDs, searchID);

                // Display result
                DisplaySearchResult(searchID, position);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        // Method to display all product IDs
        static void DisplayProductIDs(int[] productIDs)
        {
            Console.WriteLine("Available Product IDs:");
            Console.WriteLine(new string('-', 30));
            for (int i = 0; i < productIDs.Length; i++)
            {
                Console.WriteLine($"Position {i}: {productIDs[i]}");
            }
            Console.WriteLine(new string('-', 30));
        }

        // Method to get product ID from user with validation
        static int GetProductIDFromUser()
        {
            int productID = 0;
            bool validInput = false;

            while (!validInput)
            {
                Console.Write("\nEnter Product ID to search (4-digit number): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out productID))
                {
                    if (productID >= 1000 && productID <= 9999)
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Error: Product ID must be a 4-digit number (1000-9999). Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Please enter a valid number.");
                }
            }

            return productID;
        }

        // Binary Search Algorithm
        static int BinarySearch(int[] array, int target)
        {
            int left = 0;
            int right = array.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                // Display search process for educational purposes
                Console.WriteLine($"Searching range: positions {left} to {right}, middle: {array[mid]}");

                // Check if target is present at mid
                if (array[mid] == target)
                {
                    return mid; // Target found
                }

                // If target greater, ignore left half
                if (array[mid] < target)
                {
                    left = mid + 1;
                }
                // If target smaller, ignore right half
                else
                {
                    right = mid - 1;
                }
            }

            return -1; // Target not found
        }

        // Method to display search result
        static void DisplaySearchResult(int searchID, int position)
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("SEARCH RESULT");
            Console.WriteLine(new string('=', 50));

            if (position != -1)
            {
                Console.WriteLine($"✅ Product located!");
                Console.WriteLine($"Product ID: {searchID}");
                Console.WriteLine($"Position in array: {position}");
                Console.WriteLine($"Array Index: {position}");
            }
            else
            {
                Console.WriteLine($"❌ Product not found.");
                Console.WriteLine($"Product ID {searchID} is not in the inventory.");
            }

            Console.WriteLine(new string('=', 50));
        }

        // Additional method to demonstrate multiple test cases
        static void DemonstrateTestCases()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("DEMONSTRATION: BINARY SEARCH TEST CASES");
            Console.WriteLine(new string('=', 60));

            int[] productIDs = { 1023, 1145, 2040, 2750, 3189, 3650, 4021, 4578, 4900, 5120 };

            // Test Case 1: Product found (first element)
            Console.WriteLine("\n--- Test Case 1: Product Found (First) ---");
            TestBinarySearch(productIDs, 1023);

            // Test Case 2: Product found (middle element)
            Console.WriteLine("\n--- Test Case 2: Product Found (Middle) ---");
            TestBinarySearch(productIDs, 3189);

            // Test Case 3: Product found (last element)
            Console.WriteLine("\n--- Test Case 3: Product Found (Last) ---");
            TestBinarySearch(productIDs, 5120);

            // Test Case 4: Product not found
            Console.WriteLine("\n--- Test Case 4: Product Not Found ---");
            TestBinarySearch(productIDs, 3000);

            Console.WriteLine(new string('=', 60));
        }

        // Helper method for test cases
        static void TestBinarySearch(int[] array, int target)
        {
            int position = BinarySearch(array, target);
            if (position != -1)
            {
                Console.WriteLine($"Product {target} located at position {position}");
            }
            else
            {
                Console.WriteLine($"Product {target} not found");
            }
        }
    }
}