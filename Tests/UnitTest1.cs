using OrderN;
using BillN;
using MenuN;
using TableN;

namespace Tests
{
    [TestClass]
    public class BillTest
    {
        [TestMethod]
        public void GenerateBill_ShouldOutputBill()
        {
            // Arrange
            var order = new Order(); // Order instance with samples
            order.Items.Add("Item 1 - 10.00 EUR");
            order.Items.Add("Item 2 - 20.00 EUR");
            decimal totalCost = 30.00M;

            var bill = new Bill();
            StringWriter sw = new StringWriter();
            Console.SetOut(sw); // WriteLine to StringWriter
            
            // Run fn
            bill.GenerateBill(order, totalCost);
            string printedBill = sw.ToString();

            // Assert
            Assert.IsTrue(printedBill.Contains("Bill:"));
            Assert.IsTrue(printedBill.Contains("Item 1 - 10.00 EUR"));
            Assert.IsTrue(printedBill.Contains("Item 2 - 20.00 EUR"));
            Assert.IsTrue(printedBill.Contains("Total Cost: 30.00 EUR"));

            // Clean up
            sw.Close();
            Console.SetOut(Console.Out);
        }
    }


    [TestClass]
    public class MenuTests
    {
        [TestMethod]
        public void ShowMenu_ShouldOutputMenuItems()
        {
            // Arrange
            string sampleMenuFilePath = "menu.txt";
            var menu = new Menu(sampleMenuFilePath);
            var expectedOutput = new StringWriter();
            Console.SetOut(expectedOutput); // WriteLine to StringWriter

            // Run fn
            menu.ShowMenu();
            string printedMenu = expectedOutput.ToString();

            // Assert
            Assert.IsTrue(printedMenu.Contains("Menu:"));

            // Clean up
            expectedOutput.Close();
            Console.SetOut(Console.Out);
        }
    }
    [TestClass]
    public class RestaurantTests
    {
        [TestMethod]
        public void TestInitializeTables()
        {
            // Arrange
            TableManager table = new TableManager(); // Restaurant instance

            // Run fn
            table.InitializeTables();

            // Assert
            // Check if the tables list has been initialized and contains 10 tables
            Assert.IsNotNull(table.tables);
            Assert.AreEqual(10, table.tables.Count);
        }
    }

}