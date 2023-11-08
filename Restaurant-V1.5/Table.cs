using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableN
{
    public class Table
    {
        public int TableNumber { get; private set; }
        public string Status { get; set; }

        public Table(int tableNumber)
        {
            TableNumber = tableNumber;
            Status = "Available";
        }
    }
    public class TableManager
    {
        public List<Table> tables;

        public TableManager()
        {
            InitializeTables();
        }

        public void InitializeTables()
        {
            tables = new List<Table>();
            for (int i = 1; i <= 10; i++)
            {
                tables.Add(new Table(i));
            }
        }

        public void ShowTableStatus()
        {
            Console.Clear();
            Console.WriteLine("Table Status:");
            foreach (var table in tables)
            {
                Console.WriteLine($"Table {table.TableNumber}: {table.Status}");
            }
        }

        public void FreeUpTable()
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

        public int ReserveTable()
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
    }
}
