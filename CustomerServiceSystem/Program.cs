using System;
using System.Collections.Generic;

namespace CustomerServiceSystem
{
    // Customer Node Class
    class Customer
    {
        public string CustomerID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Customer Next { get; set; }

        public Customer(string id, string name, string phone)
        {
            CustomerID = id;
            Name = name;
            PhoneNumber = phone;
            Next = null;
        }

        public void DisplayCustomer()
        {
            Console.WriteLine($"| {CustomerID,-12} | {Name,-15} | {PhoneNumber,-15} |");
        }
    }

    // Singly Linked List Implementation
    class CustomerList
    {
        private Customer head;

        public CustomerList()
        {
            head = null;
        }

        // Insert operation
        public void Insert(string id, string name, string phone)
        {
            Customer newCustomer = new Customer(id, name, phone);

            if (head == null)
            {
                head = newCustomer;
            }
            else
            {
                Customer current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newCustomer;
            }
            Console.WriteLine($"Customer {name} inserted successfully!");
        }

        // Delete operation
        public Customer Delete(string id)
        {
            if (head == null)
            {
                Console.WriteLine("Customer list is empty!");
                return null;
            }

            // If head needs to be deleted
            if (head.CustomerID == id)
            {
                Customer deletedCustomer = head;
                head = head.Next;
                Console.WriteLine($"Customer {deletedCustomer.Name} deleted successfully!");
                return deletedCustomer;
            }

            // Search for customer to delete
            Customer current = head;
            while (current.Next != null)
            {
                if (current.Next.CustomerID == id)
                {
                    Customer deletedCustomer = current.Next;
                    current.Next = current.Next.Next;
                    Console.WriteLine($"Customer {deletedCustomer.Name} deleted successfully!");
                    return deletedCustomer;
                }
                current = current.Next;
            }

            Console.WriteLine($"Customer with ID {id} not found!");
            return null;
        }

        // Display operation
        public void Display()
        {
            if (head == null)
            {
                Console.WriteLine("No customers in the list!");
                return;
            }

            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("CUSTOMER RECORDS");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine("| {0,-12} | {1,-15} | {2,-15} |", "Customer ID", "Name", "Phone Number");
            Console.WriteLine(new string('-', 60));

            Customer current = head;
            while (current != null)
            {
                current.DisplayCustomer();
                current = current.Next;
            }
            Console.WriteLine(new string('=', 60));
        }
    }

    class Program
    {
        // Stack for Undo Operation
        private static Stack<Customer> undoStack = new Stack<Customer>();

        // Queue for Service Simulation
        private static Queue<string> serviceQueue = new Queue<string>();

        // Linked List for customer records
        private static CustomerList customerList = new CustomerList();

        static void Main(string[] args)
        {
            Console.WriteLine("=== CUSTOMER SERVICE MANAGEMENT SYSTEM ===");
            Console.WriteLine("Linear Data Structures: Linked List, Stack, and Queue\n");

            // Pre-insert three customers for testing as required
            InitializeSampleData();

            bool exit = false;
            while (!exit)
            {
                DisplayMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterCustomer();
                        break;
                    case "2":
                        DeleteCustomer();
                        break;
                    case "3":
                        UndoDelete();
                        break;
                    case "4":
                        customerList.Display();
                        break;
                    case "5":
                        JoinServiceQueue();
                        break;
                    case "6":
                        ServeNextCustomer();
                        break;
                    case "7":
                        ViewServiceQueue();
                        break;
                    case "8":
                        exit = true;
                        Console.WriteLine("Thank you for using Customer Service System!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice! Please try again.");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        // Display main menu
        static void DisplayMenu()
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("CUSTOMER SERVICE MANAGEMENT MENU");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("1. Register Customer");
            Console.WriteLine("2. Delete Customer Record");
            Console.WriteLine("3. Undo Last Deletion");
            Console.WriteLine("4. Display All Customers");
            Console.WriteLine("5. Join Service Queue");
            Console.WriteLine("6. Serve Next Customer");
            Console.WriteLine("7. View Service Queue");
            Console.WriteLine("8. Exit");
            Console.WriteLine(new string('=', 50));
            Console.Write("Enter your choice (1-8): ");
        }

        // Register new customer
        static void RegisterCustomer()
        {
            Console.WriteLine("\n--- REGISTER NEW CUSTOMER ---");

            Console.Write("Enter Customer ID: ");
            string id = Console.ReadLine();

            Console.Write("Enter Customer Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Phone Number: ");
            string phone = Console.ReadLine();

            customerList.Insert(id, name, phone);
        }

        // Delete customer record
        static void DeleteCustomer()
        {
            Console.WriteLine("\n--- DELETE CUSTOMER RECORD ---");

            Console.Write("Enter Customer ID to delete: ");
            string id = Console.ReadLine();

            Customer deletedCustomer = customerList.Delete(id);
            if (deletedCustomer != null)
            {
                // Push deleted customer to undo stack
                undoStack.Push(deletedCustomer);
                Console.WriteLine("Customer moved to undo stack.");
            }
        }

        // Undo last deletion
        static void UndoDelete()
        {
            Console.WriteLine("\n--- UNDO LAST DELETION ---");

            if (undoStack.Count > 0)
            {
                Customer lastDeleted = undoStack.Pop();
                customerList.Insert(lastDeleted.CustomerID, lastDeleted.Name, lastDeleted.PhoneNumber);
                Console.WriteLine($"Successfully restored {lastDeleted.Name}!");
            }
            else
            {
                Console.WriteLine("No deletions to undo!");
            }
        }

        // Join service queue
        static void JoinServiceQueue()
        {
            Console.WriteLine("\n--- JOIN SERVICE QUEUE ---");

            Console.Write("Enter customer name for service: ");
            string customerName = Console.ReadLine();

            serviceQueue.Enqueue(customerName);
            Console.WriteLine($"{customerName} added to service queue.");
            Console.WriteLine($"Queue position: {serviceQueue.Count}");
        }

        // Serve next customer from queue
        static void ServeNextCustomer()
        {
            Console.WriteLine("\n--- SERVE NEXT CUSTOMER ---");

            if (serviceQueue.Count > 0)
            {
                string customerName = serviceQueue.Dequeue();
                Console.WriteLine($"Now serving: {customerName}");
            }
            else
            {
                Console.WriteLine("No customers in the service queue!");
            }
        }

        // View current service queue
        static void ViewServiceQueue()
        {
            Console.WriteLine("\n--- CURRENT SERVICE QUEUE ---");

            if (serviceQueue.Count == 0)
            {
                Console.WriteLine("Service queue is empty!");
                return;
            }

            Console.WriteLine("Customers waiting for service:");
            int position = 1;
            foreach (string customer in serviceQueue)
            {
                Console.WriteLine($"{position}. {customer}");
                position++;
            }
            Console.WriteLine($"Total customers waiting: {serviceQueue.Count}");
        }

        // Initialize sample data for testing
        static void InitializeSampleData()
        {
            Console.WriteLine("Initializing sample customer data...");

            // Insert three customer records as required for testing
            customerList.Insert("CUST001", "John Kamau", "0712345678");
            customerList.Insert("CUST002", "Mary Wanjiku", "0723456789");
            customerList.Insert("CUST003", "Peter Ochieng", "0734567890");

            Console.WriteLine("Three sample customers added for testing.");
        }
    }
}