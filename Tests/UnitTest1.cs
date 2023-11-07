using OrderN;
using RestaurantN;
using ReceiptN;
using MenuN;
using OrderN;
using Table;

namespace Tests
{
    [TestClass]
    public class ReceiptTests
    {
        [TestMethod]
        public void GenerateReceipt_ShouldOutputReceipt()
        {
            // Arrange
            var order = new Order(); // Create an Order instance with sample items
            order.Items.Add("Item 1 - 10.00 EUR");
            order.Items.Add("Item 2 - 20.00 EUR");
            decimal totalCost = 30.00M;

            var receipt = new Receipt();
            StringWriter sw = new StringWriter();
            Console.SetOut(sw); // Redirect Console.WriteLine to StringWriter

            // Act
            receipt.GenerateReceipt(order, totalCost);
            string printedReceipt = sw.ToString();

            // Assert
            // You can use assertions to check if the expected output is generated
            Assert.IsTrue(printedReceipt.Contains("Receipt:"));
            Assert.IsTrue(printedReceipt.Contains("Item 1 - 10.00 EUR"));
            Assert.IsTrue(printedReceipt.Contains("Item 2 - 20.00 EUR"));
            Assert.IsTrue(printedReceipt.Contains("Total Cost: 30.00 EUR"));

            // Clean up the redirected Console output
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
            string sampleMenuFilePath = "menu.txt"; // Replace with a valid file path
            var menu = new Menu(sampleMenuFilePath); // Provide a valid menu file path
            var expectedOutput = new StringWriter();
            Console.SetOut(expectedOutput);

            // Act
            menu.ShowMenu();
            string printedMenu = expectedOutput.ToString();

            // Assert
            Assert.IsTrue(printedMenu.Contains("Menu:"));
            // You can add additional assertions to check specific menu items or formatting
            // For example, you can check if expected menu items are present in the output.

            // Clean up the redirected Console output
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
            TableManager table = new TableManager(); // Create a Restaurant instance

            // Act
            table.InitializeTables();

            // Assert
            // Check if the tables list has been initialized and contains 10 tables
            Assert.IsNotNull(table.tables);
            Assert.AreEqual(10, table.tables.Count);
        }
    }

}