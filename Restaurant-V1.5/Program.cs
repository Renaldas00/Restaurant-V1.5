//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;

//class Program
//{
//    static List<string> menu = new List<string>();
//    static List<string> reservations = new List<string>();
//    static List<string> order = new List<string>();
//    static Dictionary<int, string> tableStatus = new Dictionary<int, string>();

//    static void Main()
//    {
//        LoadMenu();
//        InitializeTableStatus();
//        while (true)
//        {
//            Console.WriteLine("Restaurant Rezervation System:");
//            Console.WriteLine("1. Table list");
//            Console.WriteLine("2. Manage tables");
//            Console.WriteLine("3. Create Rezervation");
//            Console.WriteLine("4. Exit");
//            Console.Write("Choose an actions (1-4): ");

//            if (int.TryParse(Console.ReadLine(), out int choice))
//            {
//                switch (choice)
//                {
//                    case 1:
//                        ShowTableStatus();
//                        break;
//                    case 2:
//                        FreeUpTable();
//                        break;
//                    case 3:
//                        CreateOrder();
//                        break;
//                    case 4:
//                        Environment.Exit(0);
//                        break;
//                    default:
//                        Console.WriteLine("Invalid choice, try again.");
//                        break;
//                }
//            }
//            else
//            {
//                Console.WriteLine("Invalid input, try again.");
//            }
//        }
//    }


//    static void LoadMenu()
//    {
//        if (File.Exists("menu.txt"))
//        {
//            menu = File.ReadAllLines("menu.txt").ToList();
//        }
//        else
//        {
//            Console.WriteLine("Menu file not found.");
//        }
//    }
//    static void InitializeTableStatus()
//    {
//        for (int i = 1; i <= 10; i++)
//        {
//            tableStatus[i] = "Available";
//        }
//    }
//    static void ShowTableStatus()
//    {
//        Console.Clear();
//        Console.WriteLine("Table statuses:");
//        foreach (var kvp in tableStatus)
//        {
//            Console.WriteLine($"Table {kvp.Key}: {kvp.Value}");
//        }
//    }

//    static void FreeUpTable()
//    {
//        Console.Clear();
//        Console.Write("Enter the table number you want to free: ");
//        if (int.TryParse(Console.ReadLine(), out int tableNumber) && tableNumber >= 1 && tableNumber <= 10)
//        {
//            if (tableStatus[tableNumber] == "Reserved")
//            {
//                tableStatus[tableNumber] = "Available";
//                Console.WriteLine($"Table {tableNumber} was changed to available.");
//            }
//            else
//            {
//                Console.WriteLine("This table was not reserved.");
//            }
//        }
//        else
//        {
//            Console.WriteLine("Invalid input.");
//        }
//    }

//    static int ReserveTable()
//    {
//        Console.Write("Enter the table number you want to reserve: ");
//        if (int.TryParse(Console.ReadLine(), out int tableNumber) && tableNumber >= 1 && tableNumber <= 10)
//        {
//            if (tableStatus[tableNumber] == "Reserved")
//            {
//                Console.WriteLine("This table is already reserved. Please select another table.");
//                return ReserveTable(); // Recursively prompt for another table
//            }
//            else
//            {
//                tableStatus[tableNumber] = "Reserved";
//                Console.WriteLine($"Table {tableNumber} has been successfully reserved.");
//                return tableNumber;
//            }
//        }
//        else
//        {
//            Console.WriteLine("Invalid table number input. Please select another table.");
//            return ReserveTable(); // Recursively prompt for another table
//        }
//    }
//    static void CreateOrder()
//    {
//        Console.Clear();
//        order.Clear();
//        ShowMenu();
//        int selectedTable = ReserveTable(); // Store the selected table

//        if (selectedTable == -1)
//        {
//            Console.WriteLine("No tables are available.");
//            return;
//        }
//        List<string> orderItems = new List<string>();

//        while (true)
//        {
//            Console.Write("Pick an item (write Q to end order process): ");
//            string choice = Console.ReadLine();

//            if (choice.ToUpper() == "Q")
//            {
//                if (orderItems.Count > 0)
//                {
//                    order.AddRange(orderItems);
//                    ViewOrder();
//                    break;
//                }
//                else
//                {
//                    break;
//                }
//            }

//            if (int.TryParse(choice, out int itemNumber) && itemNumber >= 1 && itemNumber <= menu.Count)
//            {
//                Console.Write("Order quantity: ");
//                if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
//                {
//                    orderItems.Add($"{menu[itemNumber - 1]} x{quantity}");
//                }
//                else
//                {
//                    Console.WriteLine("Invalid quantity input. Try again.");
//                }
//            }
//            else
//            {
//                Console.WriteLine("Invalid choice.");
//            }
//        }

//        if (orderItems.Count > 0)
//        {
//            order.AddRange(orderItems);
//            Console.WriteLine("Order successfully created.");
//        }
//    }

//    static void ShowMenu()
//    {
//        Console.WriteLine("Menu:");
//        for (int i = 0; i < menu.Count; i++)
//        {
//            Console.WriteLine($"{menu[i]}");
//        }
//    }

//    static void ViewOrder()
//    {
//        Console.WriteLine("Order summary:");
//        decimal totalCost = 0;

//        foreach (var kvp in tableStatus)
//        {
//            if (kvp.Value == "Reserved")
//            {
//                Console.WriteLine($"Reserved table {kvp.Key}");
//            }
//        }

//        foreach (string item in order)
//        {
//            string[] parts = item.Split('-');
//            if (parts.Length == 2)
//            {
//                Console.WriteLine(item);
//                decimal itemPrice = decimal.Parse(parts[1].Trim().Split(" ")[0]);
//                int quantity = int.Parse(item.Split('x')[1].Trim());
//                totalCost += itemPrice * quantity;
//            }
//        }

//        Console.WriteLine($"Total cost: {totalCost.ToString("0.00")} EUR");

//        TimeZoneInfo lietuvosTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Vilnius");
//        DateTime lietuvosLaikas = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, lietuvosTimeZone);
//        Console.WriteLine($"Order time: {lietuvosLaikas.ToString("yyyy-MM-dd HH:mm:ss")}");
//        IReceipt receiptGenerator = new ReceiptGenerator();
//        receiptGenerator.GenerateReceipt(order, totalCost);
//    }

//    public interface IReceipt
//    {
//        void GenerateReceipt(List<string> order, decimal totalCost);
//    }

//    public class ReceiptGenerator : IReceipt
//    {
//        public void GenerateReceipt(List<string> order, decimal totalCost)
//        {
//            Console.WriteLine("--------------------------------------------------------------");
//            Console.WriteLine("Bill:");
//            foreach (string item in order)
//            {
//                Console.WriteLine(item);
//            }
//            Console.WriteLine($"Total cost: {totalCost.ToString("0.00")} EUR");
//            Console.WriteLine("--------------------------------------------------------------");
//        }
//    }
//}



/////////////////////////////////////////////////////////////////// Good ////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////// Good ////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////// Good ////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////// Good ////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////// Good ////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////// Good ////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////// Good ////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////// Good ////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////// Good ////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////// Good ////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////// Good ////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////// Good ////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////// Good ////////////////////////////////////////////////////////////////
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;

//class Program
//{
//    static void Main()
//    {
//        var restaurant = new Restaurant();
//        restaurant.Run();
//    }
//}

//class Restaurant
//{
//    private List<string> menu = new List<string>();
//    private List<string> order = new List<string>();
//    private Dictionary<int, string> tableStatus = new Dictionary<int, string>();

//    public Restaurant()
//    {
//        LoadMenu();
//        InitializeTableStatus();
//    }

//    public void Run()
//    {
//        while (true)
//        {
//            Console.WriteLine("Restaurant Reservation System:");
//            Console.WriteLine("1. Table Status");
//            Console.WriteLine("2. Manage Tables");
//            Console.WriteLine("3. Create Reservation");
//            Console.WriteLine("4. Exit");
//            Console.Write("Choose an action (1-4): ");

//            if (int.TryParse(Console.ReadLine(), out int choice))
//            {
//                switch (choice)
//                {
//                    case 1:
//                        ShowTableStatus();
//                        break;
//                    case 2:
//                        FreeUpTable();
//                        break;
//                    case 3:
//                        CreateReservation();
//                        break;
//                    case 4:
//                        Environment.Exit(0);
//                        break;
//                    default:
//                        Console.WriteLine("Invalid choice, please try again.");
//                        break;
//                }
//            }
//            else
//            {
//                Console.WriteLine("Invalid input, please try again.");
//            }
//        }
//    }

//    private void LoadMenu()
//    {
//        if (File.Exists("menu.txt"))
//        {
//            menu = File.ReadAllLines("menu.txt").ToList();
//        }
//        else
//        {
//            Console.WriteLine("Menu file not found.");
//        }
//    }

//    private void InitializeTableStatus()
//    {
//        for (int i = 1; i <= 10; i++)
//        {
//            tableStatus[i] = "Available";
//        }
//    }

//    private void ShowTableStatus()
//    {
//        Console.Clear();
//        Console.WriteLine("Table Status:");
//        foreach (var kvp in tableStatus)
//        {
//            Console.WriteLine($"Table {kvp.Key}: {kvp.Value}");
//        }
//    }

//    private void FreeUpTable()
//    {
//        Console.Clear();
//        Console.Write("Enter the table number you want to free: ");
//        if (int.TryParse(Console.ReadLine(), out int tableNumber) && tableNumber >= 1 && tableNumber <= 10)
//        {
//            if (tableStatus[tableNumber] == "Reserved")
//            {
//                tableStatus[tableNumber] = "Available";
//                Console.WriteLine($"Table {tableNumber} is now available.");
//            }
//            else
//            {
//                Console.WriteLine("This table was not reserved.");
//            }
//        }
//        else
//        {
//            Console.WriteLine("Invalid input.");
//        }
//    }

//    private int ReserveTable()
//    {
//        Console.Write("Enter the table number you want to reserve: ");
//        if (int.TryParse(Console.ReadLine(), out int tableNumber) && tableNumber >= 1 && tableNumber <= 10)
//        {
//            if (tableStatus[tableNumber] == "Reserved")
//            {
//                Console.WriteLine("This table is already reserved. Please select another table.");
//                return ReserveTable(); // Recursively prompt for another table
//            }
//            else
//            {
//                tableStatus[tableNumber] = "Reserved";
//                Console.WriteLine($"Table {tableNumber} has been successfully reserved.");
//                return tableNumber;
//            }
//        }
//        else
//        {
//            Console.WriteLine("Invalid table number input.");
//            return ReserveTable();
//        }
//    }

//    private void CreateReservation()
//    {
//        Console.Clear();
//        order.Clear();
//        ShowMenu();
//        ReserveTable();

//        List<string> orderItems = new List<string>();

//        while (true)
//        {
//            Console.Write("Select an item (type 'Q' to end the order process): ");
//            string choice = Console.ReadLine();

//            if (choice.ToUpper() == "Q")
//            {
//                if (orderItems.Count > 0)
//                {
//                    order.AddRange(orderItems);
//                    ViewOrder();
//                    break;
//                }
//                else
//                {
//                    break;
//                }
//            }

//            if (int.TryParse(choice, out int itemNumber) && itemNumber >= 1 && itemNumber <= menu.Count)
//            {
//                Console.Write("Order quantity: ");
//                if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
//                {
//                    orderItems.Add($"{menu[itemNumber - 1]} x{quantity}");
//                }
//                else
//                {
//                    Console.WriteLine("Invalid quantity input. Try again.");
//                }
//            }
//            else
//            {
//                Console.WriteLine("Invalid choice.");
//            }
//        }

//        if (orderItems.Count > 0)
//        {
//            order.AddRange(orderItems);
//            Console.WriteLine("Reservation successfully created.");
//        }
//    }

//    private void ShowMenu()
//    {
//        Console.WriteLine("Menu:");
//        for (int i = 0; i < menu.Count; i++)
//        {
//            Console.WriteLine($"{menu[i]}");
//        }
//    }

//    private void ViewOrder()
//    {
//        Console.WriteLine("Order Summary:");
//        decimal totalCost = 0;

//        foreach (var kvp in tableStatus)
//        {
//            if (kvp.Value == "Reserved")
//            {
//                Console.WriteLine($"Reserved Table {kvp.Key}");
//            }
//        }

//        foreach (string item in order)
//        {
//            string[] parts = item.Split('-');
//            if (parts.Length == 2)
//            {
//                Console.WriteLine(item);
//                decimal itemPrice = decimal.Parse(parts[1].Trim().Split(" ")[0]);
//                int quantity = int.Parse(item.Split('x')[1].Trim());
//                totalCost += itemPrice * quantity;
//            }
//        }

//        Console.WriteLine($"Total Cost: {totalCost.ToString("0.00")} EUR");

//        TimeZoneInfo lietuvosTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Vilnius");
//        DateTime lietuvosLaikas = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, lietuvosTimeZone);
//        Console.WriteLine($"Order Time: {lietuvosLaikas.ToString("yyyy-MM-dd HH:mm:ss")}");
//        IReceipt receiptGenerator = new ReceiptGenerator();
//        receiptGenerator.GenerateReceipt(order, totalCost);
//    }

//    private interface IReceipt
//    {
//        void GenerateReceipt(List<string> order, decimal totalCost);
//    }

//    private class ReceiptGenerator : IReceipt
//    {
//        public void GenerateReceipt(List<string> order, decimal totalCost)
//        {
//            Console.WriteLine("--------------------------------------------------------------");
//            Console.WriteLine("Bill:");
//            foreach (string item in order)
//            {
//                Console.WriteLine(item);
//            }
//            Console.WriteLine($"Total Cost: {totalCost.ToString("0.00")} EUR");
//            Console.WriteLine("--------------------------------------------------------------");
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        var restaurant = new Restaurant();
        restaurant.Run();
    }
}

class Menu
{
    private List<string> items;

    public Menu(string menuFilePath)
    {
        if (File.Exists(menuFilePath))
        {
            items = File.ReadAllLines(menuFilePath).ToList();
        }
        else
        {
            Console.WriteLine("Menu file not found.");
            items = new List<string>();
        }
    }

    public void ShowMenu()
    {
        Console.WriteLine("Menu:");
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i]}");
        }
    }

    public string GetMenuItem(int itemNumber)
    {
        if (itemNumber >= 1 && itemNumber <= items.Count)
        {
            return items[itemNumber - 1];
        }
        else
        {
            return null;
        }
    }
}

class Table
{
    public int TableNumber { get; private set; }
    public string Status { get; set; }

    public Table(int tableNumber)
    {
        TableNumber = tableNumber;
        Status = "Available";
    }
}

class Order
{
    public List<string> Items { get; private set; }

    public Order()
    {
        Items = new List<string>();
    }

    public void AddItem(string item)
    {
        Items.Add(item);
    }

    public void Clear()
    {
        Items.Clear();
    }
}

class Bill
{
    public void GenerateBill(Order order, decimal totalCost)
    {
        Console.WriteLine("--------------------------------------------------------------");
        Console.WriteLine("Bill:");
        foreach (string item in order.Items)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine($"Total Cost: {totalCost.ToString("0.00")} EUR");
        Console.WriteLine("--------------------------------------------------------------");
    }
}

class Receipt
{
    public void GenerateReceipt(Order order, decimal totalCost)
    {
        Console.WriteLine("Receipt:");
        foreach (string item in order.Items)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine($"Total Cost: {totalCost.ToString("0.00")} EUR");
    }
}

class Restaurant
{
    private Menu menu;
    private List<Table> tables;
    private Order order;

    public Restaurant()
    {
        menu = new Menu("menu.txt");
        InitializeTables();
        order = new Order();
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("Restaurant Reservation System:");
            Console.WriteLine("1. Table Status");
            Console.WriteLine("2. Manage Tables");
            Console.WriteLine("3. Create Reservation");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an action (1-4): ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        ShowTableStatus();
                        break;
                    case 2:
                        FreeUpTable();
                        break;
                    case 3:
                        CreateReservation();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input, please try again.");
            }
        }
    }

    private void InitializeTables()
    {
        tables = new List<Table>();
        for (int i = 1; i <= 10; i++)
        {
            tables.Add(new Table(i));
        }
    }

    private void ShowTableStatus()
    {
        Console.Clear();
        Console.WriteLine("Table Status:");
        foreach (var table in tables)
        {
            Console.WriteLine($"Table {table.TableNumber}: {table.Status}");
        }
    }

    private void FreeUpTable()
    {
        Console.Clear();
        Console.Write("Enter the table number you want to free: ");
        if (int.TryParse(Console.ReadLine(), out int tableNumber) && tableNumber >= 1 && tableNumber <= 10)
        {
            Table table = tables.Find(t => t.TableNumber == tableNumber);
            if (table != null && table.Status == "Reserved")
            {
                table.Status = "Available";
                Console.WriteLine($"Table {tableNumber} is now available.");
            }
            else
            {
                Console.WriteLine("This table was not reserved.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }
    }

    private void CreateReservation()
    {
        Console.Clear();
        order.Clear();
        menu.ShowMenu();
        int tableNumber = ReserveTable();

        List<string> orderItems = new List<string>();

        while (true)
        {
            Console.Write("Select an item (type 'Q' to end the order process): ");
            string choice = Console.ReadLine();

            if (choice.ToUpper() == "Q")
            {
                if (orderItems.Count > 0)
                {
                    order.Items.AddRange(orderItems);
                    ViewOrder(tableNumber);
                    break;
                }
                else
                {
                    break;
                }
            }

            if (int.TryParse(choice, out int itemNumber))
            {
                string menuItem = menu.GetMenuItem(itemNumber);
                if (menuItem != null)
                {
                    Console.Write("Order quantity: ");
                    if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                    {
                        orderItems.Add($"{menuItem} x{quantity}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid quantity input. Try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }
            }
        }

        if (orderItems.Count > 0)
        {
            order.Items.AddRange(orderItems);
            Console.WriteLine("Reservation successfully created.");
        }
    }

    private int ReserveTable()
    {
        Console.Write("Enter the table number you want to reserve: ");
        if (int.TryParse(Console.ReadLine(), out int tableNumber) && tableNumber >= 1 && tableNumber <= 10)
        {
            Table table = tables.Find(t => t.TableNumber == tableNumber);
            if (table != null && table.Status == "Available")
            {
                table.Status = "Reserved";
                Console.WriteLine($"Table {tableNumber} has been successfully reserved.");
                return tableNumber;
            }
            else
            {
                Console.WriteLine("This table is already reserved. Please select another table.");
                return ReserveTable();
            }
        }
        else
        {
            Console.WriteLine("Invalid table number input.");
            return ReserveTable();
        }
    }

    private void ViewOrder(int tableNumber)
    {
        Console.WriteLine("Order Summary:");
        decimal totalCost = 0;

        Console.WriteLine($"Reserved Table {tableNumber}");

        foreach (string item in order.Items)
        {
            string[] parts = item.Split('-');
            if (parts.Length == 2)
            {
                Console.WriteLine(item);
                decimal itemPrice = decimal.Parse(parts[1].Trim().Split(" ")[0]);
                int quantity = int.Parse(item.Split('x')[1].Trim());
                totalCost += itemPrice * quantity;
            }
        }

        Console.WriteLine($"Total Cost: {totalCost.ToString("0.00")} EUR");

        TimeZoneInfo lietuvosTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Vilnius");
        DateTime lietuvosLaikas = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, lietuvosTimeZone);
        Console.WriteLine($"Order Time: {lietuvosLaikas.ToString("yyyy-MM-dd HH:mm:ss")}");

        Bill billGenerator = new Bill();
        billGenerator.GenerateBill(order, totalCost);

        //Receipt receiptGenerator = new Receipt();
        //receiptGenerator.GenerateReceipt(order, totalCost);
    }
}
