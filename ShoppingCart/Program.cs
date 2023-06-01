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
            private decimal salesTaxRate;

            //Getters&Setters
            public List<Product> Items
            {
                get { return items; }
                set { items = value; }
            }
            public decimal SalesTaxRate
            {
                get { return salesTaxRate; }
                set { salesTaxRate = value; }
            }

            //Constructors
            public ShoppingCart()
            {
                Items = new List<Product>();
            }
            public ShoppingCart(decimal salesTaxRate)
            {
                this.SalesTaxRate = salesTaxRate;
                Items = new List<Product>();
            }

            //Functions
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
            public decimal CalculateSalesTax()//Sale tax for total price
            {
                decimal totalPrice = CalculateTotalPrice();
                decimal totalTax = totalPrice * salesTaxRate;
                decimal roundedtotalTax = Decimal.Round(totalTax, 2);
                return roundedtotalTax;
            }
        }
        
        static void Main(string[] args)
        {
            decimal salesTaxRate = 0.125m;

            ShoppingCart cart1 = new ShoppingCart();
            ShoppingCart cart2 = new ShoppingCart();
            ShoppingCart cart3 = new ShoppingCart(salesTaxRate);

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

            //STEP 2:
            Console.WriteLine("Step 2: \n");
            //Create Product
            Product doveSoap2 = new Product
            {
                Name = "Dove Soap",
                Price = 39.99m,
                Quantity = 5
            };
            Product doveSoap2i = new Product
            {
                Name = "Dove Soap",
                Price = 39.99m,
                Quantity = 3
            };
            //Add Items
            cart2.Items.Add(doveSoap2);
            cart2.Items.Add(doveSoap2i);
            //Calculations
            decimal totalPrice2 = cart2.CalculateTotalPrice();

            Console.WriteLine("Shopping Cart:");
            Console.WriteLine($"- Total Price: {totalPrice2}\n");

            //================================================================================//

            //STEP 3:
            Console.WriteLine("Step 3: \n");
            //Create Product
            Product doveSoap3 = new Product
            {
                Name = "Dove Soap",
                Price = 39.99m,
                Quantity = 2
            };
            Product axeDeo3 = new Product
            {
                Name = "Axe Deo",
                Price = 99.99m,
                Quantity = 2
            };
            //Add Items
            cart3.Items.Add(doveSoap3);
            cart3.Items.Add(axeDeo3);
            //Calculations
            decimal totalPrice3 = cart3.CalculateTotalPrice();
            decimal totalTax3 = cart3.CalculateSalesTax();

            Console.WriteLine("Shopping Cart:");
            Console.WriteLine($"- Total Price: {totalPrice3 + totalTax3}");
            Console.WriteLine($"- Total Tax: {totalTax3}\n");

            //================================================================================//
            
            Console.ReadLine();
        }
    }
}
