using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuN
{
    public class Menu
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
                Console.WriteLine(items[i]);
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
}

