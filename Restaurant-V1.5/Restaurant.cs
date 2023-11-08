using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableN;
using OrderN;
namespace RestaurantN
{
    public class Restaurant
    {
        // Instances of TableManager and OrderManager
        private TableManager tableManager;
        private OrderManager orderManager;

        public Restaurant()
        {
            // Initialize the TableManager and OrderManager instances
            tableManager = new TableManager();
            orderManager = new OrderManager(tableManager);
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
                            tableManager.ShowTableStatus();
                            break;
                        case 2:
                            tableManager.FreeUpTable();
                            break;
                        case 3:
                            orderManager.CreateReservation();
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
    }

}
