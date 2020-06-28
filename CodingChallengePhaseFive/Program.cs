using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;

namespace CodingChallengePhaseFive
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayBanner("INVENTORY MANAGEMENT SYSTEM");

            // Main Menu Orchestation
            Dictionary<int, string> mainMenu = new Dictionary<int, string>();
            Dictionary<int, string> maintenanceMenu = new Dictionary<int, string>();
            Dictionary<int, string> reportMenu = new Dictionary<int, string>();

            // Build Menu Collection
            BuildMainMenu(mainMenu);
            BuildMaintenanceMenu(maintenanceMenu);
            BuildReportMenu(reportMenu);

            // Inventory List
            List<string> inventoryItems = new List<string>();
            List<double> inventoryPrices = new List<double>();
            List<int> inventoryCounts = new List<int>();

            int exitCode = 3;
            int returnCode = 4;
            int menuSelection = 0;

            while (menuSelection != exitCode)
            {
                DisplayMenuOptions(mainMenu);
                menuSelection = GetMenuSelection(mainMenu.Keys.Min(), mainMenu.Keys.Max());
                int secondarySelection = 0;

                switch (menuSelection)
                {
                    case 1:
                        while (secondarySelection != returnCode)
                        {
                            Console.Clear();
                            DisplayMenuOptions(maintenanceMenu);
                            secondarySelection = GetMenuSelection(maintenanceMenu.Keys.Min(), maintenanceMenu.Keys.Max());
                            HandleMaintenanceSelection(secondarySelection, inventoryCounts, inventoryItems, inventoryPrices);
                        }
                        break;
                    case 2:
                        while (secondarySelection != returnCode)
                        {
                            Console.Clear();
                            DisplayMenuOptions(reportMenu);
                            secondarySelection = GetMenuSelection(reportMenu.Keys.Min(), reportMenu.Keys.Max());
                            HandleReportSelection(secondarySelection,inventoryCounts,inventoryItems,inventoryPrices);
                        }
                        break;
                }

                Console.Clear();
            }

            ExitApplication();
        }

        private static void HandleReportSelection(int secondarySelection, List<int> inventoryCounts, List<string> inventoryItems, List<double> inventoryPrices)
        {
            List<string[]> inventoryReport = new List<string[]>();

            inventoryReport.Add(new string[] { "", "", "" });

            switch (secondarySelection)
            {
                case 1: // All Inventory
                    BuildReportForAllItems(inventoryCounts, inventoryItems, inventoryPrices, inventoryReport);
                    DisplayReport(inventoryReport, "All Inventory Items");
                    break;
                case 2: // Price Range
                    BuildReportByItemPriceRange(inventoryReport, inventoryItems, inventoryPrices, inventoryCounts);
                    DisplayReport(inventoryReport, "Items Within Price Range.");
                    break;
                case 3: // Count Range
                    BuildReportByItemCountRange(inventoryReport, inventoryItems, inventoryPrices, inventoryCounts);
                    DisplayReport(inventoryReport, "Items Within Count Range.");
                    break;
            }

        }

        private static void BuildReportForAllItems(List<int> inventoryCounts, List<string> inventoryItems, List<double> inventoryPrices, List<string[]> inventoryReport)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                inventoryReport.Add(new string[] { inventoryItems[i], inventoryPrices[i].ToString(), inventoryCounts[i].ToString() });
            }
        }

        private static void BuildReportByItemPriceRange(List<string[]> inventoryReport, List<string> inventoryItems, List<double> inventoryPrices, List<int> inventoryCounts)
        {
            Console.WriteLine("What is the minimum price you want to see? Invalid input will default to 1.");
            double minPrice = 1.0;
            try
            {
                minPrice = double.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
               // Swallowing to keep default value;
            }
            Console.WriteLine("What is the maximum price you want to see? Invalid input will default to 1000.");
            double maxPrice = 1000;
            try
            {
                maxPrice = double.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                // Swallowing to keep default value;
            }

            for (int i = 0; i < inventoryPrices.Count; i++)
            {
                double priceCheck = inventoryPrices[i];

                if (priceCheck >= minPrice && priceCheck <  maxPrice)
                {
                    inventoryReport.Add(new string[] { inventoryItems[i], inventoryPrices[i].ToString(), inventoryCounts[i].ToString() });
                }                
            }
        }

        private static void BuildReportByItemCountRange(List<string[]> inventoryReport, List<string> inventoryItems, List<double> inventoryPrices, List<int> inventoryCounts)
        {
            Console.WriteLine("What is the minimum count you want to see? Invalid input will default to 1.");
            int minCount = 1;
            try
            {
                minCount = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                // Swallowing to keep default value;
            }
            Console.WriteLine("What is the maximum count you want to see? Invalid input will default to 100.");
            int maxCount = 100;
            try
            {
                maxCount = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                // Swallowing to keep default value;
            }

            for (int i = 0; i < inventoryCounts.Count; i++)
            {
                int countCheck = inventoryCounts[i];

                if (countCheck >= minCount && countCheck <= maxCount)
                {
                    inventoryReport.Add(new string[] { inventoryItems[i], inventoryPrices[i].ToString(), inventoryCounts[i].ToString() });
                }
            }
        }

        private static void DisplayReport(List<string[]> inventoryReport, string title)
        {
            Console.Clear();
            DisplayBanner(title);
            Console.WriteLine();
            string[] headers = new string[] { "Name", "Price", "Count" };
            Console.WriteLine($"{headers[0],-25} {headers[1],-25} {headers[2],-25}");

            foreach (var item in inventoryReport)
            {
                Console.WriteLine($"{item[0],-25} {item[1],-25} {item[2],-25}" );
            }

            Console.WriteLine();
            PauseApplication();
        }

        private static void HandleMaintenanceSelection(int secondarySelection, List<int> inventoryCounts, List<string> inventoryItems, List<double> inventoryPrices)
        {
            switch (secondarySelection)
            {
                case 1: // Add Inventory
                    AddInventory(inventoryItems, inventoryPrices, inventoryCounts);
                    break;
                case 2: // Update Inventory
                    HandleInventoryChange(inventoryItems, inventoryPrices, inventoryCounts,"update");
                    break;
                case 3: // Remove Inventory
                    HandleInventoryChange(inventoryItems, inventoryPrices, inventoryCounts);
                    break;
            }           
        }

        private static void HandleInventoryChange(List<string> inventoryItems, List<double> inventoryPrices, List<int> inventoryCounts, string updateType = "remove")
        {
            bool goAgain = false;
            
            do
            {
                Console.Clear();
                bool isBadInput = false;
                if (inventoryItems.Count == 0)
                {
                    Console.WriteLine("There are no items in your inventory. Returning to the menu.");
                    PauseApplication();
                    break;
                }
                // Display the current items with their index number + 1
                Console.WriteLine("These are the items currently in your inventory.");
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    Console.WriteLine($"[{i+1}] - {inventoryItems[i]}");
                }
                Console.WriteLine($"Enter the number associated with the item you want to {updateType}. An invalid entry will cancel the action." );
                int itemSelection ;

                try
                {
                    itemSelection = int.Parse(Console.ReadLine());
                    if (itemSelection < 1 || itemSelection > inventoryItems.Count)
                    {
                        isBadInput = true;
                    }
                    else
                    {
                        switch (updateType)
                        {
                            case "remove":
                                RemoveItem(inventoryItems, inventoryPrices, inventoryCounts, itemSelection);
                                break;
                            case "update":
                                UpdateItem(inventoryItems, inventoryPrices, inventoryCounts, itemSelection);
                                break;
                        }
                        
                    }
                }
                catch (Exception)
                {
                    isBadInput = true;                
                }

                if (isBadInput)
                {
                    Console.WriteLine("Your input is invalid, this action has been cancelled.");
                    PauseApplication();
                    goAgain = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"Would you like to {updateType} another item (y/n)?");
                    goAgain = Console.ReadLine().Trim().ToLower() == "y" ? true : false;
                }
            } while (goAgain == true);
           
        }

        private static void UpdateItem(List<string> inventoryItems, List<double> inventoryPrices, List<int> inventoryCounts, int itemSelection)
        {
            string itemName = inventoryItems[itemSelection - 1];
            double itemPrice = inventoryPrices[itemSelection - 1];
            int itemCount = inventoryCounts[itemSelection - 1];

            Console.WriteLine($"You have chosen to change the following item.");
            Console.WriteLine($"Name: {itemName} {Environment.NewLine} Price: {itemPrice}  {Environment.NewLine} Count: {itemCount}.");
            Console.WriteLine("I will ask you for new values. If you want to keep the current value just press the enter key.");
            Console.WriteLine();

            // Item Name
            Console.WriteLine("What is the name of the of the item?");
            string userInput = Console.ReadLine().Trim();
            itemName = userInput == "" ? itemName : userInput;

            // Item Price
            Console.WriteLine("What is the price of the item? An invalid entry will default to 1.99.");
            userInput = Console.ReadLine().Trim();
            if (userInput == "")
            {
                goto UpdateCount;
            }

            try
            {
                itemPrice = double.Parse(userInput);
            }
            catch (FormatException)
            {

                itemPrice = 1.99;
            }

            UpdateCount:

            Console.WriteLine("What is the count of the item? An invalid entry will default to 1.");
            userInput = Console.ReadLine().Trim();
            if (userInput == "")
            {
                goto UpdateLists;
            }

            try
            {
                itemCount = int.Parse(userInput);
            }
            catch (FormatException)
            {

                itemCount = 1;
            }

        UpdateLists:

            inventoryItems[itemSelection - 1] = itemName;
            inventoryPrices[itemSelection - 1] = itemPrice;
            inventoryCounts[itemSelection - 1] = itemCount;

            Console.WriteLine($"The item values are:");
            Console.WriteLine($"Name: {itemName} {Environment.NewLine} Price: {itemPrice}  {Environment.NewLine} Count: {itemCount}.");
        }

        private static void RemoveItem(List<string> inventoryItems, List<double> inventoryPrices, List<int> inventoryCounts, int itemSelection)
        {
            string itemToRemove = inventoryItems[itemSelection - 1];
            inventoryItems.RemoveAt(itemSelection - 1);
            inventoryCounts.RemoveAt(itemSelection - 1);
            inventoryPrices.RemoveAt(itemSelection - 1);

            Console.WriteLine($"{itemToRemove} has been removed from your inventory.");
        }

        private static void AddInventory(List<string> inventoryItems, List<double> inventoryPrices, List<int> inventoryCounts)
        {
            bool addNewItem = false;

            do
            {
                Console.Clear();
                Console.WriteLine("To add a new item, you will need three peices of information:");
                Console.WriteLine("1: The name of the item, this value is mandatory.");
                Console.WriteLine("2: The cost of the item. Any invalid data will default to 1.99.");
                Console.WriteLine("3: Hom many of the item do you have in a stock. Invalid data will default to 1.");
                Console.WriteLine();

                string itemName = "";
                while (string.IsNullOrWhiteSpace(itemName) == true)
                {
                    Console.WriteLine("What is the name of the item?");
                    itemName = Console.ReadLine();
                }


                Console.WriteLine("What is the sale price for the item.");
                double salePrice = 0;
                try
                {
                    salePrice = double.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    salePrice = 1.99;
                }

                Console.WriteLine("How many of the item do you have in stock?");
                int itemCount = 0;
                try
                {
                    itemCount = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    itemCount = 1;
                }

                // Add item information to the lists
                inventoryItems.Add(itemName);
                inventoryPrices.Add(salePrice);
                inventoryCounts.Add(itemCount);

                Console.WriteLine($"{itemName} has been added to the inventory with a price of {salePrice:C} and a count of {itemCount}");
                Console.WriteLine();
                Console.WriteLine("Would you like to add another item (y/n)?");

                string user = "";

                addNewItem = Console.ReadLine().Trim().ToLower() == "y" ? true : false;

                string bestTeacher = user == "Kate" ? "Brian" : "Scott";

                if (Console.ReadLine() == "y") 
                {
                    addNewItem = true;
                }
                else
                {
                    addNewItem = false;
                }


            } while (addNewItem);
        }     

        private static void BuildReportMenu(Dictionary<int, string> reportMenu)
        {
            reportMenu.Add(1, "View All Items"); 
            reportMenu.Add(2, "View Items Within a Price Range");
            reportMenu.Add(3, "View Items With Count Filter");
            reportMenu.Add(4, "Return to Main Menu");
        }

        private static void BuildMaintenanceMenu(Dictionary<int, string> maintenanceMenu)
        {
            maintenanceMenu.Add(1, "Add Inventory");
            maintenanceMenu.Add(2, "Update Inventory Item");
            maintenanceMenu.Add(3, "Remove Inventory Item");
            maintenanceMenu.Add(4, "Return to Main Menu");
        }

        private static int GetMenuSelection(int minValue, int maxValue)
        {
            int selection = 0;
            while (selection == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Enter the numeric value for your desired action.");
                try
                {
                    selection = int.Parse(Console.ReadLine());


                    if (selection < minValue || selection > maxValue)
                    {
                        Console.WriteLine();
                        Console.WriteLine("You have entered an invalid number.");
                        selection = 0;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine();
                    Console.WriteLine("You must enter the numeric value of the action.");
                }

            }

            return selection;
        }

        private static void DisplayMenuOptions(Dictionary<int, string> menu)
        {
            Console.WriteLine();
            Console.WriteLine("What action would you like to take?");
            Console.WriteLine();
            foreach (var item in menu)
            {
                string message = $"[{item.Key}] - {item.Value}";
                Console.WriteLine(message.PadRight(50));
            }

        }

        private static void BuildMainMenu(Dictionary<int, string> mainMenu)
        {
            mainMenu.Add(1, "Inventory Maintenance");
            mainMenu.Add(2, "View Inventory");
            mainMenu.Add(3, "Exit Application");
        }

        private static void DisplayBanner(string message)
        {
            Console.Clear();
            Console.WriteLine(new string('*', 50));
            int buffer = 24 - message.Length / 2;
            Console.WriteLine("*" + new string(' ', buffer) + message + new string(' ', buffer - 1) + "*");
            Console.WriteLine(new string('*', 50));
        }

        private static void PauseApplication()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void ExitApplication()
        {
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
