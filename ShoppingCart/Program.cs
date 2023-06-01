using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    internal class Program
    {
        public class Product
        {
            //Members
            private string name;
            private decimal price;
            private int quantity;

            //Getters&Setters
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            public decimal Price
            {
                get { return price; }
                set { price = value; }
            }

            public int Quantity
            {
                get { return quantity; }
                set { quantity = value; }
            }
        }

        public class ShoppingCart
        {
            //Members
            private List<Product> items;

            //Getters&Setters
            public List<Product> Items
            {
                get { return items; }
                set { items = value; }
            }

            //Constructors
            public ShoppingCart()
            {
                Items = new List<Product>();
            }

            public decimal CalculateTotalPrice()//Calculate totalprice of shoppingcart without any deductions
            {
                decimal totalPrice = 0;
                decimal roundedtotalPrice = 0;
                foreach (var item in Items)
                {
                    decimal itemPrice = item.Price * item.Quantity;
                    totalPrice += itemPrice;
                    roundedtotalPrice = Decimal.Round(totalPrice, 2);
                }

                return roundedtotalPrice;
            }

        }
        
        static void Main(string[] args)
        {
            ShoppingCart cart1 = new ShoppingCart();

            //STEP 1:
            Console.WriteLine("Step 1: \n");
            //Create Product
            Product doveSoap1 = new Product
            {
                Name = "Dove Soap",
                Price = 39.99m,
                Quantity = 5
            };
            //Add Items
            cart1.Items.Add(doveSoap1);
            //Calculations
            decimal totalPrice1 = cart1.CalculateTotalPrice();

            Console.WriteLine("Shopping Cart:");
            Console.WriteLine($"- Total Price: {totalPrice1}\n");

            //================================================================================//

            Console.ReadLine();
        }
    }
}
