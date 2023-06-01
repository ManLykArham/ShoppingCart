namespace ShoppingCart.UnitTest
{
    [TestClass]
    public class ShoppingCartTest
    {
        [TestMethod]
        public void TestFor_Step1()
        {
            //Arrange
            ShoppingCart cart1 = new ShoppingCart();

            Product doveSoap1 = new Product
            {
                Name = "Dove Soap",
                Price = 39.99m,
                Quantity = 5
            };

            //Act
            cart1.Items.Add(doveSoap1);
            decimal totalPrice = cart1.CalculateTotalPrice();

            //Assert
            Assert.AreEqual(199.95m, totalPrice);
        }

        [TestMethod]
        public void TestFor_Step2()
        {
            //Arrange
            ShoppingCart cart2 = new ShoppingCart();

            Product doveSoap2 = new Product
            {
                Name = "Dove Soap",
                Price = 39.99m,
                Quantity = 5,
                BuyQuantity = 0,
                GetFreeQuantity = 0
            };
            Product doveSoap2i = new Product
            {
                Name = "Dove Soap",
                Price = 39.99m,
                Quantity = 3,
                BuyQuantity = 0,
                GetFreeQuantity = 0
            };

            //Act
            cart2.Items.Add(doveSoap2);
            cart2.Items.Add(doveSoap2i);
            decimal totalPrice2 = cart2.CalculateTotalPrice();

            //Assert
            Assert.AreEqual(319.92m, totalPrice2);
        }

        [TestMethod]
        public void TestFor_Step3()
        {
            //Arrange
            decimal salesTaxRate3 = 0.125m;
            ShoppingCart cart3 = new ShoppingCart(salesTaxRate3);

            Product doveSoap3 = new Product
            {
                Name = "Dove Soap",
                Price = 39.99m,
                Quantity = 2,
                BuyQuantity = 0,
                GetFreeQuantity = 0
            };
            Product axeDeo3 = new Product
            {
                Name = "Axe Deo",
                Price = 99.99m,
                Quantity = 2,
                BuyQuantity = 0,
                GetFreeQuantity = 0
            };

            //Act
            cart3.Items.Add(doveSoap3);
            cart3.Items.Add(axeDeo3);
            decimal totalPrice3 = cart3.CalculateTotalPrice();
            decimal totalTax3 = cart3.CalculateSalesTax();
            decimal finaltotalPrice3 = totalPrice3 + totalTax3;

            //Assert
            Assert.AreEqual(314.96m, finaltotalPrice3);
            Assert.AreEqual(35.00m, totalTax3);
        }

        [TestMethod]
        public void TestFor_Step4()
        {
            //Arrange
            decimal salesTaxRate4 = 0.125m;
            ShoppingCart cart4 = new ShoppingCart(salesTaxRate4);
            ShoppingCart cart4i = new ShoppingCart(salesTaxRate4);


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
                Quantity = 2,
                BuyQuantity = 0,
                GetFreeQuantity = 0
            };

            //Act
            cart4.Items.Add(doveSoap4);//Adding 3 dove soaps to cart4

            decimal totalPrice4 = cart4.CalculateTotalPrice();
            decimal totalDiscount4 = cart4.CalculateTotalDiscount();
            decimal totalTax4 = cart4.CalculateSalesTaxDP();
            decimal finaltotalPrice4 = totalPrice4 - totalDiscount4;

            cart4.Items.Add(doveSoap4i);//Adding 2 more dove soaps sp total amount == 5, in cart4
            decimal totalPrice4i = cart4.CalculateTotalPrice();
            decimal totalDiscount4i = cart4.CalculateTotalDiscount();
            decimal totalTax4i = cart4.CalculateSalesTaxDP();
            decimal finaltotalPrice4i = totalPrice4i - totalDiscount4i;

            cart4i.Items.Add(doveSoap4ii);
            cart4i.Items.Add(axeDeo4);

            decimal totalPrice4ii = cart4i.CalculateTotalPrice();
            decimal totalDiscount4ii = cart4i.CalculateTotalDiscount();
            decimal totalTax4ii = cart4i.CalculateSalesTaxDP();
            decimal finaltotalPrice4ii = (totalPrice4ii - totalDiscount4ii) + totalTax4ii;

            //Assert
            Assert.AreEqual(79.98m, finaltotalPrice4);
            Assert.AreEqual(39.99m, totalDiscount4);
            Assert.AreEqual(10.00m, totalTax4);

            Assert.AreEqual(159.96m, finaltotalPrice4i);
            Assert.AreEqual(39.99m, totalDiscount4i);
            Assert.AreEqual(20.00m, totalTax4i);

            Assert.AreEqual(292.46m, finaltotalPrice4ii);
            Assert.AreEqual(39.99m, totalDiscount4ii);
            Assert.AreEqual(32.50m, totalTax4ii);
        }

        [TestMethod]
        public void TestFor_Step5()
        {
            //Arrange
            decimal salesTaxRate5 = 0.125m;
            ShoppingCart cart5 = new ShoppingCart(salesTaxRate5);


            Product doveSoap5 = new Product
            {
                Name = "Dove Soap",
                Price = 39.99m,
                Quantity = 2,
                BuyQuantity = 1,
                Discount = 50
            };

            //Act
            cart5.Items.Add(doveSoap5);

            decimal totalPrice5 = cart5.CalculateTotalPrice();
            decimal totalDiscount5 = cart5.CalculateTotalDiscount();
            decimal totalTax5 = cart5.CalculateSalesTaxDP();
            decimal finaltotalPrice5 = (totalPrice5 - totalDiscount5) + totalTax5;

            //Assert
            Assert.AreEqual(67.48m, finaltotalPrice5);
            Assert.AreEqual(20.00m, totalDiscount5);
            Assert.AreEqual(7.50m, totalTax5);
        }

        [TestMethod]
        public void TestFor_Step6()
        {
            //Arrange
            decimal salesTaxRate6 = 0.125m;
            decimal discountThreshold6 = 500;
            decimal discountPercentage6 = 20;
            ShoppingCart cart6 = new ShoppingCart(salesTaxRate6, discountThreshold6, discountPercentage6);


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

            //Act
            cart6.Items.Add(doveSoap6);
            cart6.Items.Add(axeDeo6);

            decimal totalPrice6 = cart6.CalculateTotalPrice();
            decimal totalglobalDiscount6 = cart6.CalculateGlobalDiscount();
            decimal totalTax6 = cart6.CalculateSalesTaxGD();
            decimal finaltotalPrice6 = (totalPrice6 - totalglobalDiscount6) + totalTax6;

            //Assert
            Assert.AreEqual(503.92m, finaltotalPrice6);
            Assert.AreEqual(111.98m, totalglobalDiscount6);
            Assert.AreEqual(55.99m, totalTax6);
        }
    }
}