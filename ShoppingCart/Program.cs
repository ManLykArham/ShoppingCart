using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    
        public class Product
        {
            //Members
            private string name;
            private decimal price;
            private int quantity;
            private int buyQuantity;
            private int getFreeQuantity;
            private decimal discount;

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
            public int BuyQuantity
            {
                get { return buyQuantity; }
                set { buyQuantity = value; }
            }
            public int GetFreeQuantity
            {
                get { return getFreeQuantity; }
                set { getFreeQuantity = value; }
            }
            public decimal Discount
            {
                get { return discount; }
                set { discount = value; }
            }
        }

        //Class - Blueprint to create an Object
        public class ShoppingCart 
        {
            //Members -  Variables defined inside a class that hold data
            private List<Product> items;
            private decimal salesTaxRate;
            private decimal discountPercentage;
            private decimal discountThreshold;

            //Getters&Setters - Properties that control access to class fields, `public List<Product> Items` allows controlled access to items
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
            public decimal DiscountPercentage
            {
                get { return discountPercentage; }
                set { discountPercentage = value; }
            }
            public decimal DiscountThreshold
            {
                get { return discountThreshold; }
                set { discountThreshold = value; }
            }

            //Constructors - Used to initialize objects
            public ShoppingCart() // Initializing ShoppingCart
            {
                Items = new List<Product>();
            }
            public ShoppingCart(decimal salesTaxRate) // Initializing ShoppingCart with specific value `salesTaxRate`
            {
                this.SalesTaxRate = salesTaxRate;
                Items = new List<Product>();
            }
            public ShoppingCart(decimal salesTaxRate, decimal discountThreshold, decimal discountPercentage) //Initializing ShoppingCart with specific value `salesTaxRate, discountThreshold, discountPercentage`
            {
                this.SalesTaxRate = salesTaxRate;
                this.DiscountThreshold = discountThreshold;
                this.DiscountPercentage = discountPercentage;
                Items = new List<Product>();
            }

            //Functions
            public decimal CalculateTotalPrice() //Calculates totalprice of ShoppingCart without any deductions
            {
                decimal totalPrice = 0;
                decimal roundedtotalPrice = 0;
                foreach (var item in Items) //Selecting each Product from the list of items in `Items`
                {
                    decimal itemPrice = item.Price * item.Quantity; //Storing the item price in `itemPrice` by multiplying the items price and quantity
                    totalPrice += itemPrice; //Storing the total sum of the products price in `totalPrice`
                    roundedtotalPrice = Decimal.Round(totalPrice, 2); //Rounding total price to 2 decimal places
                }
                return roundedtotalPrice; //This function will return the `roundedtotalPrice`
            }
            public decimal CalculateSalesTax() //Calculates Sale tax for total price
            {
                decimal totalPrice = CalculateTotalPrice(); //Gets and stores the total sum of the products price by calling the `CalculateTotalPrice()` function
                decimal totalTax = totalPrice * salesTaxRate; //Total Tax is calculated by multiplying the `totalPrice` by the `salesTaxRate`
                decimal roundedtotalTax = Decimal.Round(totalTax, 2); //Rounding totalTax to 2 decimal places
                return roundedtotalTax; //This function will return the `roundedtotalTax`
        }
            public decimal CalculateSalesTaxDP()//Calculates sale tax for totalprice - discountprice (not just the total price)
            {
                decimal totalPrice = CalculateTotalPrice(); //Total price is obtained from the `CalculateTotalPrice()` function
                decimal totalDiscount = CalculateTotalDiscount(); //TotalDiscount is obtained by calling the `CalculateTotalDiscount()` function
                decimal discountedPrice = totalPrice - totalDiscount;
                decimal totalTax = discountedPrice * salesTaxRate;
                decimal roundedtotalTax = Decimal.Round(totalTax, 2);
                return roundedtotalTax;
            }
            public decimal CalculateSalesTaxGD()//Calculates Sale tax for total price - globalDiscount(GD)
            {
                decimal totalPrice = CalculateTotalPrice();
                decimal totalglobalDiscount = CalculateGlobalDiscount();
                decimal discountedPrice = totalPrice - totalglobalDiscount;
                decimal totalTax = discountedPrice * salesTaxRate;
                decimal roundedtotalTax = Decimal.Round(totalTax, 2);
                return roundedtotalTax;
            }
            public decimal CalculateTotalDiscount() //Calculates total discount
            {
                decimal totalDiscount = 0;
                decimal roundedtotalDiscount = 0;
                foreach (var item in Items) //Lopping through each product in the Items list of products
                {
                    if (!(item.Quantity > 0 && item.BuyQuantity <= 0 && item.GetFreeQuantity <= 0)) //Checks if the product is eligible for a discount (Quantity > 0, BuyQuantity > 0, GetFreeQuantity > 0)
                {
                        int setsOfBuyGet = item.Quantity / (item.BuyQuantity + item.GetFreeQuantity); //Calculates how many sets of "Buy X, Get Y Free" the customer can get
                        decimal itemDiscount = (setsOfBuyGet * item.GetFreeQuantity * item.Price); //Calculates the discount from the free items

                    if (item.Discount > 0) //If there is an additional percentage discount on the item
                    {
                        int eligibleItems = item.Quantity / 2; //Calculates the number of items eligible for the Buy One, Get Discount offer
                        decimal itemBuyOneGetDiscount = eligibleItems * item.Price * (item.Discount / 100); //Calculates the additional discount for the eligible items
                        itemDiscount += itemBuyOneGetDiscount; //Add this additional discount to the item discount
                    }

                        totalDiscount += itemDiscount; //Adds the item discount to the total discount
                        roundedtotalDiscount = Decimal.Round(totalDiscount, 2); //Rounds the total discount to 2 decimal places for accuracy
                }
                }

                return roundedtotalDiscount; //Return the rounded total discount
        }
        public decimal CalculateGlobalDiscount() //Calculates global discount
            {
                decimal totalPrice = CalculateTotalPrice();
                decimal globalDiscount = (totalPrice * discountPercentage) / 100;
                decimal roundedglobalDiscount = Decimal.Round(globalDiscount, 2);

                return roundedglobalDiscount;
            }
        }
    internal class Program
    {
        static void Main(string[] args)
        {
            decimal salesTaxRate = 0.125m; //Setting sales tax rate
            decimal discountThreshold = 500; //Setting discount threshold
            decimal discountPercentage = 20; //Setting discount percentage

            //Creating different types of ShoppingCarts
            ShoppingCart cart1 = new ShoppingCart();
            ShoppingCart cart2 = new ShoppingCart();
            ShoppingCart cart3 = new ShoppingCart(salesTaxRate);
            ShoppingCart cart4 = new ShoppingCart(salesTaxRate);
            ShoppingCart cart4i = new ShoppingCart(salesTaxRate);
            ShoppingCart cart5 = new ShoppingCart(salesTaxRate);
            ShoppingCart cart6 = new ShoppingCart(salesTaxRate, discountThreshold, discountPercentage);


            //STEP 1:
            Console.WriteLine("Step 1: \n");
            //Creating Product
            Product doveSoap1 = new Product
            {
                Name = "Dove Soap",
                Price = 39.99m,
                Quantity = 5
            };
            //Adding Items into cart
            cart1.Items.Add(doveSoap1);
            //Calculations
            decimal totalPrice1 = cart1.CalculateTotalPrice();

            Console.WriteLine("Shopping Cart:");
            Console.WriteLine($"- Total Price: {totalPrice1}\n");

            //================================================================================//

            //STEP 2:
            Console.WriteLine("Step 2: \n");
            //Creating Products
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
            //Adding Items into cart
            cart2.Items.Add(doveSoap2);
            cart2.Items.Add(doveSoap2i);
            //Calculations
            decimal totalPrice2 = cart2.CalculateTotalPrice();

            Console.WriteLine("Shopping Cart:");
            Console.WriteLine($"- Total Price: {totalPrice2}\n");

            //================================================================================//

            //STEP 3:
            Console.WriteLine("Step 3: \n");
            //Creating Products
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
            //Adding Items into cart
            cart3.Items.Add(doveSoap3);
            cart3.Items.Add(axeDeo3);
            //Calculations
            decimal totalPrice3 = cart3.CalculateTotalPrice();
            decimal totalTax3 = cart3.CalculateSalesTax();

            Console.WriteLine("Shopping Cart:");
            Console.WriteLine($"- Total Price: {totalPrice3 + totalTax3}");
            Console.WriteLine($"- Total Tax: {totalTax3}\n");

            //================================================================================//

            //STEP 4:
            Console.WriteLine("Step 4: \n");
            //Creating Products
            Product doveSoap4 = new Product
            {
                Name = "Dove Soap",
                Price = 39.99m,
                Quantity = 3,
                BuyQuantity = 2,
                GetFreeQuantity = 1
            };
            Product doveSoap4i = new Product
            {
                Name = "Dove Soap",
                Price = 39.99m,
                Quantity = 2,
                BuyQuantity = 2,
                GetFreeQuantity = 1
            };
            Product doveSoap4ii = new Product
            {
                Name = "Dove Soap",
                Price = 39.99m,
                Quantity = 3,
                BuyQuantity = 2,
                GetFreeQuantity = 1
            };
            Product axeDeo4 = new Product
            {
                Name = "Axe Deo",
                Price = 89.99m,
                Quantity = 2
            };
            //Adding Items into cart
            cart4.Items.Add(doveSoap4);
            //Calculations
            decimal totalPrice4 = cart4.CalculateTotalPrice();
            decimal totalDiscount4 = cart4.CalculateTotalDiscount();
            decimal totalTax4 = cart4.CalculateSalesTaxDP();

            Console.WriteLine("Shopping Cart:");
            Console.WriteLine($"- Total Price: {totalPrice4 - totalDiscount4}");
            Console.WriteLine($"- Total Discount: {totalDiscount4}");
            Console.WriteLine($"- Total Tax: {totalTax4}\n");
            //Adding Items into cart
            cart4.Items.Add(doveSoap4i);
            //Calculations
            decimal totalPrice4i = cart4.CalculateTotalPrice();
            decimal totalDiscount4i = cart4.CalculateTotalDiscount();
            decimal totalTax4i = cart4.CalculateSalesTaxDP();

            Console.WriteLine("Shopping Cart:");
            Console.WriteLine($"- Total Price: {totalPrice4i - totalDiscount4i}");
            Console.WriteLine($"- Total Discount: {totalDiscount4i}");
            Console.WriteLine($"- Total Tax: {totalTax4i}\n");
            //Adding Items into cart
            cart4i.Items.Add(doveSoap4ii);
            cart4i.Items.Add(axeDeo4);
            //Calculations
            decimal totalPrice4ii = cart4i.CalculateTotalPrice();
            decimal totalDiscount4ii = cart4i.CalculateTotalDiscount();
            decimal totalTax4ii = cart4i.CalculateSalesTaxDP();

            Console.WriteLine("Shopping Cart:");
            Console.WriteLine($"- Total Price: {(totalPrice4ii - totalDiscount4ii) + totalTax4ii}");
            Console.WriteLine($"- Total Discount: {totalDiscount4ii}");
            Console.WriteLine($"- Total Tax: {totalTax4ii}\n");

            //================================================================================//

            //Step 5:
            Console.WriteLine("Step 5: ");
            //Creating Product
            Product doveSoap5 = new Product
            {
                Name = "Dove Soap",
                Price = 39.99m,
                Quantity = 2,
                BuyQuantity = 1,
                Discount = 50
            };
            //Adding Items into cart
            cart5.Items.Add(doveSoap5);
            //Calculations
            decimal totalPrice5 = cart5.CalculateTotalPrice();
            decimal totalDiscount5 = cart5.CalculateTotalDiscount();
            decimal totalTax5 = cart5.CalculateSalesTaxDP();

            Console.WriteLine("Shopping Cart:");
            Console.WriteLine($"- Total Price: {(totalPrice5 - totalDiscount5) + totalTax5}");
            Console.WriteLine($"- Total Discount: {totalDiscount5}");
            Console.WriteLine($"- Total Tax: {totalTax5}\n");

            //================================================================================//

            //Step 6
            Console.WriteLine("Step 6: ");
            //Creating Products
            Product doveSoap6 = new Product
            {
                Name = "Dove Soap",
                Price = 39.99m,
                Quantity = 5
            };
            Product axeDeo6 = new Product
            {
                Name = "Dove Soap",
                Price = 89.99m,
                Quantity = 4
            };
            //Adding Items into cart
            cart6.Items.Add(doveSoap6);
            cart6.Items.Add(axeDeo6);
            //Calculations
            decimal totalPrice6 = cart6.CalculateTotalPrice();
            decimal totalglobalDiscount6 = cart6.CalculateGlobalDiscount();
            decimal totalTax6 = cart6.CalculateSalesTaxGD();

            Console.WriteLine("Shopping Cart:");
            Console.WriteLine($"- Total Price: {(totalPrice6 - totalglobalDiscount6) + totalTax6}");
            Console.WriteLine($"- Total Discount: {totalglobalDiscount6}");
            Console.WriteLine($"- Total Tax: {totalTax6}\n");

            Console.ReadLine();
        }
    }
}
