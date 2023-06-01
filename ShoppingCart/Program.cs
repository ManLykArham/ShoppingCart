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

        public class ShoppingCart
        {
            //Members
            private List<Product> items;
            private decimal salesTaxRate;
            private decimal discountPercentage;
            private decimal discountThreshold;

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
            public ShoppingCart(decimal salesTaxRate, decimal discountThreshold, decimal discountPercentage)
            {
                this.SalesTaxRate = salesTaxRate;
                this.DiscountThreshold = discountThreshold;
                this.DiscountPercentage = discountPercentage;
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
            public decimal CalculateSalesTaxDP()//sale tax for totalprice - discountprice (not just the total price)
            {
                decimal totalPrice = CalculateTotalPrice();
                decimal totalDiscount = CalculateTotalDiscount();
                decimal discountedPrice = totalPrice - totalDiscount;
                decimal totalTax = discountedPrice * salesTaxRate;
                decimal roundedtotalTax = Decimal.Round(totalTax, 2);
                return roundedtotalTax;
            }
            public decimal CalculateSalesTaxGD()//sale tax for total price - globalDiscount(GD)
            {
                decimal totalPrice = CalculateTotalPrice();
                decimal totalglobalDiscount = CalculateGlobalDiscount();
                decimal discountedPrice = totalPrice - totalglobalDiscount;
                decimal totalTax = discountedPrice * salesTaxRate;
                decimal roundedtotalTax = Decimal.Round(totalTax, 2);
                return roundedtotalTax;
            }
            public decimal CalculateTotalDiscount()
            {
                decimal totalDiscount = 0;
                decimal roundedtotalDiscount = 0;
                foreach (var item in Items)
                {
                    if (!(item.Quantity > 0 && item.BuyQuantity <= 0 && item.GetFreeQuantity <= 0))
                    {
                        int setsOfBuyGet = item.Quantity / (item.BuyQuantity + item.GetFreeQuantity);
                        int remainingItems = item.Quantity % (item.BuyQuantity + item.GetFreeQuantity);
                        decimal itemDiscount = (setsOfBuyGet * item.GetFreeQuantity * item.Price) + (remainingItems * item.Price * (discountPercentage / 100));

                        if (item.Discount > 0)
                        {
                            int eligibleItems = item.Quantity / 2;
                            decimal itemBuyOneGetDiscount = eligibleItems * item.Price * (item.Discount / 100);
                            itemDiscount += itemBuyOneGetDiscount;
                        }

                        totalDiscount += itemDiscount;
                        roundedtotalDiscount = Decimal.Round(totalDiscount, 2);
                    }
                }

                return roundedtotalDiscount;
            }
            public decimal CalculateGlobalDiscount()
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
            decimal salesTaxRate = 0.125m;
            decimal discountThreshold = 500;
            decimal discountPercentage = 20;

            ShoppingCart cart1 = new ShoppingCart();
            ShoppingCart cart2 = new ShoppingCart();
            ShoppingCart cart3 = new ShoppingCart(salesTaxRate);
            ShoppingCart cart4 = new ShoppingCart(salesTaxRate);
            ShoppingCart cart4i = new ShoppingCart(salesTaxRate);
            ShoppingCart cart5 = new ShoppingCart(salesTaxRate);
            ShoppingCart cart6 = new ShoppingCart(salesTaxRate, discountThreshold, discountPercentage);


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

            //STEP 4:
            Console.WriteLine("Step 4: \n");
            //Create Product
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
            //Add Items
            cart4.Items.Add(doveSoap4);
            //Calculations
            decimal totalPrice4 = cart4.CalculateTotalPrice();
            decimal totalDiscount4 = cart4.CalculateTotalDiscount();
            decimal totalTax4 = cart4.CalculateSalesTaxDP();

            Console.WriteLine("Shopping Cart:");
            Console.WriteLine($"- Total Price: {totalPrice4 - totalDiscount4}");
            Console.WriteLine($"- Total Discount: {totalDiscount4}");
            Console.WriteLine($"- Total Tax: {totalTax4}\n");
            //Add Items
            cart4.Items.Add(doveSoap4i);
            //Calculations
            decimal totalPrice4i = cart4.CalculateTotalPrice();
            decimal totalDiscount4i = cart4.CalculateTotalDiscount();
            decimal totalTax4i = cart4.CalculateSalesTaxDP();

            Console.WriteLine("Shopping Cart:");
            Console.WriteLine($"- Total Price: {totalPrice4i - totalDiscount4i}");
            Console.WriteLine($"- Total Discount: {totalDiscount4i}");
            Console.WriteLine($"- Total Tax: {totalTax4i}\n");
            //Add Items
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

            Product doveSoap5 = new Product
            {
                Name = "Dove Soap",
                Price = 39.99m,
                Quantity = 2,
                BuyQuantity = 1,
                Discount = 50
            };
            //Add Items
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
            //Add Items
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
