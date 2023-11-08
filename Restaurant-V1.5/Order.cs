using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableN;
using MenuN;
using BillN;

namespace OrderN
{
    public class Order
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

    public class OrderManager
    {
        private TableManager tableManager;
        private Menu menu;
        private Order order;

        // Constructor to initialize the order manager
        public OrderManager(TableManager tableManager)
        {
            this.tableManager = tableManager;
            menu = new Menu("menu.txt");
            order = new Order();
        }

        public void CreateReservation()
        {
            Console.Clear();
            order.Clear();
            menu.ShowMenu();
            int tableNumber = tableManager.ReserveTable();

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

        public void ViewOrder(int tableNumber)
        {
            Console.WriteLine("Order Summary:");
            decimal totalCost = 0;

            Console.WriteLine($"Reserved Table {tableNumber}");

            foreach (string item in order.Items)
            {
                string[] parts = item.Split('-'); //
                if (parts.Length == 2)
                {
                    Console.WriteLine(item);
                    decimal itemPrice = decimal.Parse(parts[1].Trim().Split(" ")[0]);
                    int quantity = int.Parse(item.Split('x')[1].Trim());
                    totalCost += itemPrice * quantity;
                }
            }

            Console.WriteLine($"Total Cost: {totalCost.ToString("0.00")} EUR");
            // just get pc time
            TimeZoneInfo ltTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Vilnius");
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, ltTimeZone);
            Console.WriteLine($"Order Time: {dateTime.ToString("yyyy-MM-dd HH:mm:ss")}");

            // Instance to generate a bill
            IBillGenerator billGenerator = new Bill();
            billGenerator.GenerateBill(order, totalCost);
        }
    }
}
