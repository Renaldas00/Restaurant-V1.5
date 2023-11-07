using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill
{
    public class Bill
    {
        public void GenerateBill(OrderN.Order order, decimal totalCost)
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
}
