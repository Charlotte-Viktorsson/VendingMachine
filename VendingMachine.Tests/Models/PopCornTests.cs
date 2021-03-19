using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using VendingMachine.Models;

namespace VendingMachine.Tests.Models
{
    public class PopCornTests
    {
        [Fact]
        public void NormalConstructorCheckAllFieldsTest()
        {
            //Arrange
            string name1 = "Estrella";
            string info1 = "1 kg";
            string usage = "Eat the popcorn and have fun!";
            int price1 = 45;

            //Act
            var pop1 = new PopCorn(price1, name1, info1);

            //Assert
            Assert.Equal(name1, pop1.Name);
            Assert.Equal(info1, pop1.Info);
            Assert.Equal(usage, pop1.UsageDescription);
            Assert.Equal(price1, pop1.Price);

        }

        [Fact]
        public void NormalConstructorCheckAllMethodsTest()
        {
            //Arrange
            string name = "OLW";
            string info = "500 g";
            string usage = "Eat the popcorn and have fun!";
            int price = 18;

            //Act
            var pop1 = new PopCorn(price, name, info);
            string examineString = pop1.Examine();
            string usageString = pop1.Use();

            //Assert
            //check Examine method
            Assert.Contains(name, examineString);
            Assert.Contains(info, examineString);
            Assert.Contains(price.ToString(), examineString);

            //check Use method
            Assert.Contains(usage, usageString);
        }

        [Fact]
        public void ConstructorWithNullTest()
        {
            //Arrange
            string ExpectedName = "Unknown name";
            string usage = "Eat the popcorn and have fun!";
            string ExpectedInfo = "Information unknown";
            int price = 20;

            //Act
            var pop1 = new PopCorn(price, null, null);

            //Assert
            Assert.Equal(ExpectedName, pop1.Name);
            Assert.Equal(ExpectedInfo, pop1.Info);
            Assert.Equal(usage, pop1.UsageDescription); //since this is set in constructor
        }

        [Fact]
        public void ConstructorWithEmptyStringsTest()
        {
            //Arrange
            string ExpectedName = "Unknown name";
            string usage = "Eat the popcorn and have fun!";
            string ExpectedInfo = "Information unknown";
            int price = 20;

            //Act
            var pop1 = new PopCorn(price, "", "");

            //Assert
            Assert.Equal(ExpectedName, pop1.Name);
            Assert.Equal(ExpectedInfo, pop1.Info);
            Assert.Equal(usage, pop1.UsageDescription); //since this is set in constructor
        }

        [Fact]
        public void ConstructorWithWrongPriceTest()
        {
            //Arrange
            int expectedPrice = 20;
            int price = -20;

            //Act
            var popcorn = new PopCorn(price, "", "");

            //Assert
            Assert.Equal(expectedPrice, popcorn.Price);
        }

        [Fact]
        public void PropertyNameTest()
        {
            //Arrange
            string name = "Hemmakväll";
            string expectedNullName = "Unknown name";
            int price = 20;

            //Act
            var popcorn = new PopCorn(price, name, "");
            string initialName = popcorn.Name;

            popcorn.Name = null;
            string nullName = popcorn.Name;

            popcorn.Name = "";
            string emptyName = popcorn.Name;

            popcorn.Name = name;
            string changedName = popcorn.Name;

            //Assert
            Assert.Equal(name, initialName);
            Assert.Equal(expectedNullName, nullName);
            Assert.Equal(expectedNullName, emptyName);
            Assert.Equal(changedName, name);
        }

        [Fact]
        public void PropertyInformationTest()
        {
            //Arrange
            string info = "av majs";
            string expectedNullInfo = "Information unknown";
            int price = 20;

            //Act
            var popcorn = new PopCorn(price, "Estrella", info);
            string initialInfo = popcorn.Info;

            popcorn.Info = null;
            string nullName = popcorn.Info;

            popcorn.Info = "";
            string emptyName = popcorn.Info;

            popcorn.Info = info;
            string changedName = popcorn.Info;

            //Assert
            Assert.Equal(info, initialInfo);
            Assert.Equal(expectedNullInfo, nullName);
            Assert.Equal(expectedNullInfo, emptyName);
            Assert.Equal(changedName, info);
        }

        [Fact]
        public void PropertyPriceTest()
        {
            //Arrange
            string name = "OLW";
            string info = "";
            int price = 10;
            int expectedNegativePrice = 20;
            int negativePrice = -20;
            var popcorn = new PopCorn(price, name, info);

            //Act

            int initialPrice = popcorn.Price;

            //set it to a negative price

            popcorn.Price = negativePrice;
            int receivedNegativePrice = popcorn.Price;

            //set price to 0
            popcorn.Price = 0;
            int zeroPrice = popcorn.Price;

            //try normal update of price
            popcorn.Price = price;
            int updatedPrice = popcorn.Price;

            //Assert
            Assert.Equal(price, initialPrice);
            Assert.Equal(expectedNegativePrice, receivedNegativePrice);
            Assert.Equal(0, zeroPrice);
            Assert.Equal(price, updatedPrice);
        }

        [Fact]
        public void PropertyUsageDescriptionTest()
        {
            //Arrange
            string info = "OLW";
            string expectedNullUsageDescription = "Unknown usage";
            string initialDefaultDescription = "Eat the popcorn and have fun!";
            int price = 20;

            var popcorn = new PopCorn(price, info, info);
            //Act

            string initialDescr = popcorn.UsageDescription; //what is set by constructor

            popcorn.UsageDescription = null;
            string nullDescr = popcorn.UsageDescription;

            popcorn.UsageDescription = "";
            string emptyDescr = popcorn.UsageDescription;

            popcorn.UsageDescription = info;
            string changedDescr = popcorn.UsageDescription;

            string usageDescriptionFromUse = popcorn.Use();

            //Assert
            Assert.Equal(initialDefaultDescription, initialDescr);
            Assert.Equal(expectedNullUsageDescription, nullDescr);
            Assert.Equal(expectedNullUsageDescription, emptyDescr);
            Assert.Equal(info, changedDescr);
            Assert.Equal(info, usageDescriptionFromUse);
        }

        [Fact]
        public void MethodUseTest()
        {
            //Arrange
            var popcorn = new PopCorn(900, "OLW", "Info");
            string expectedInitialUsage = "Eat the popcorn and have fun!";
            string expectedChangedUsage = "Updated usage";

            //Act
            string receivedIntialUsage = popcorn.Use();
            popcorn.UsageDescription = expectedChangedUsage;


            //Assert
            Assert.Equal(expectedInitialUsage, receivedIntialUsage);
            Assert.Equal(expectedChangedUsage, popcorn.Use());
        }

        [Fact]
        public void MethodExamineTest()
        {
            //Arrange
            var pop1 = new PopCorn(43, "Estrella", "Made in Sweden");
            string expectedExaminationContent1 = "Estrella \t\t43 kr. \tMade in Sweden";

            //Act
            string actualUsage = pop1.Examine();

            //Assert
            Assert.Equal(expectedExaminationContent1, actualUsage);
        }
    }
}
