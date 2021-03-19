using System;
using Xunit;
using VendingMachine.Models;
using VendingMachine.Data;

namespace VendingMachine.Data.Tests
{
    public class VendingControllerTests
    {
        [Fact]
        public void ConstructorWithAdditionTest()
        {
            //Arrange
            int initialPayment = 0;
            string lastChoice = "Choice 7";

            //Act
            var vm = new VendingController(true);

            //Assert
            Assert.Equal(initialPayment, vm.Payment);
            Assert.Contains(lastChoice, vm.ShowAll());
        }


        [Fact]
        public void ConstructorNoProductsAddedTest()
        {
            //Arrange
            int initialPayment = 0;
            string choice = "choice";

            //Act
            var vm = new VendingController(false);

            //Assert
            Assert.Equal(initialPayment, vm.Payment);
            Assert.DoesNotContain(choice, vm.ShowAll());
        }

        [Fact]
        public void PropertyPaymentTest()
        {
            //Arrange
            var vm1 = new VendingController(true);
            int expectedInitialPayment = 0;
            int expectedChangedPayment = 20;

            var vm2 = new VendingController(false);

            //Act
            int actualInitialPayment = vm1.Payment;

            //set method is private, can only be set from other methods in the class
            bool goodInsert = vm1.InsertMoney(20);
            int actualChangedPayment = vm1.Payment;

            //item 0 costs more than available... expects Payment to be unchanged
            string purchaceResult = vm1.Purchase(0);//item 0 costs more than available... expects Payment to be unchanged
            int actualPayment = vm1.Payment;

            bool insertResult = vm1.InsertMoney(-100); //-100 is not in denomination, should not be accepted
            int actualPaymentAfterFailedInsert = vm1.Payment;

            //Assert
            Assert.Equal(expectedInitialPayment, actualInitialPayment);
            Assert.True(goodInsert);
            Assert.Equal(expectedChangedPayment, actualChangedPayment);
            Assert.Equal(actualChangedPayment, actualPayment);
            Assert.Contains("More money needed", purchaceResult);
            Assert.Equal(actualChangedPayment, actualPaymentAfterFailedInsert);
            Assert.False(insertResult);
        }


        [Theory]
        [InlineData(1, true, 1)]
        [InlineData(10, true, 10)]
        [InlineData(20, true, 20)]
        [InlineData(50, true, 50)]
        [InlineData(100, true, 100)]
        [InlineData(500, true, 500)]
        [InlineData(1000, true, 1000)]
        [InlineData(11, false, 0)]
        [InlineData(0, false, 0)]
        [InlineData(200, false, 0)]
        public void InsertMoneyTest(int amount, bool expectedResult, int expectedAmount)
        {
            //arrange
            var vm = new VendingController(false);

            //act
            bool insertResult = vm.InsertMoney(amount);

            //assert
            Assert.Equal(insertResult, expectedResult);
            Assert.Equal(expectedAmount, vm.Payment);
        }

        [Fact]
        public void InsertMoneySeveralTimesTest()
        {
            //arrange
            var vm = new VendingController(false);
            var expInitialMoney = 0;
            var expFirstBuyRemains = 20;
            var expSecondBuyRemains = 20;
            var expThirdBuyRemains = 20;

            //act
            var actInitialMoney = vm.Payment;

            bool insertResult1 = vm.InsertMoney(20);
            var actFirstInsertRemainders = vm.Payment;

            bool insertResult2 = vm.InsertMoney(-50); //negative value, not in denominations
            var actSecondInsertRemainders = vm.Payment;

            bool insertResult3 = vm.InsertMoney(133); //not in denominations
            var actThirdInsertRemainders = vm.Payment;

            //assert
            Assert.Equal(expInitialMoney, actInitialMoney);
            Assert.Equal(expFirstBuyRemains, actFirstInsertRemainders);
            Assert.Equal(expSecondBuyRemains, actSecondInsertRemainders);
            Assert.Equal(expThirdBuyRemains, actThirdInsertRemainders);
            Assert.True(insertResult1);
            Assert.False(insertResult2);
            Assert.False(insertResult3);
        }


        [Theory]
        [InlineData(20, 10, "Product not found")]
        [InlineData(20, 0, "More money needed for this product")]
        [InlineData(20, 1, "Here is your product:")]

        public void PurchaseTest(int availableMoney, int choice, string expectedResult)
        {
            //Arrange
            var vm = new VendingController(true);
            vm.InsertMoney(availableMoney);

            //Act
            string actualReturnString = vm.Purchase(choice);

            //Assert
            Assert.Contains(expectedResult, actualReturnString);
        }


        [Fact]
        public void MultiplePurchasesTest()
        {
            //Arrange
            var vm = new VendingController(true);
            var expInitialMoney = 0;
            var expInsertMoney1 = 10;
            var expInsertMoney2 = 10;

            //Act
            var actInitialMoney = vm.Payment;
            vm.InsertMoney(10);
            var actInsertMoney1 = vm.Payment;
            vm.InsertMoney(-1); //should not be accepted since it is not in denominations
            var actInsertMoney2 = vm.Payment;  //money should remain the same

            string returnString = vm.Purchase(1); //purchase item 1 with price 15 (more money needed)
            var actPurchase1Remainder = vm.Payment;
            string returnString2 = vm.Purchase(7); //purchase item 7 with price 10 
            var actPurchase2Remainder = vm.Payment;

            //Assert
            Assert.Equal(expInitialMoney, actInitialMoney);
            Assert.Equal(expInsertMoney1, actInsertMoney1);
            Assert.Equal(expInsertMoney2, actInsertMoney2);
            Assert.Contains("More money needed", returnString);
            Assert.Equal(expInsertMoney2, actPurchase1Remainder);
            Assert.Contains("Water", returnString2);
            Assert.Equal(0, actPurchase2Remainder);
        }


        [Theory]
        [InlineData(1000, 20, 1000, "1000 kr: 1")]//invalid choice 20, should get all in return

        [InlineData(1000, 0, 975, "1000 kr: 0")]//choice 0 costs 25, 975 in return
        [InlineData(1000, 0, 975, "500 kr: 1")]
        [InlineData(1000, 0, 975, "100 kr: 4")]
        [InlineData(1000, 0, 975, "50 kr: 1")]
        [InlineData(1000, 0, 975, "20 kr: 1")]
        [InlineData(1000, 0, 975, "10 kr: 0")]
        [InlineData(1000, 0, 975, "5 kr: 1")]
        [InlineData(1000, 0, 975, "1 kr: 0")]

        [InlineData(10, 20, 10, "10 kr: 1")] //invalid choice 20, should get all in return
        [InlineData(50, 3, 24, "1 kr: 4")] //choice 3 costs 26, 24 in return
        [InlineData(50, 3, 24, "20 kr: 1")]
        public void EndTransactionTest(int availableMoney, int choice, int expChange, string expResult)
        {
            //Arrange
            var vm = new VendingController(true);
            vm.InsertMoney(availableMoney);
            vm.Purchase(choice);
            int actualPayment = vm.Payment;
            //Act
            string actualResult = vm.EndTransaction();

            //Assert
            Assert.Equal(expChange, actualPayment);
            Assert.Contains(expResult, actualResult);
        }

    }
}
