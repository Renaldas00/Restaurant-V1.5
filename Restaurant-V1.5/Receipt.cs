using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderN;
namespace ReceiptN
{
    public class Receipt
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
}
