using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using VendingMachine.Models;

namespace VendingMachine.Tests.Models
{
    public class SodaTests
    {

        [Fact]
        public void NormalConstructorCheckAllFieldsTest()
        {
            //Arrange
            string name1 = "Coca Cola";
            string info1 = "1 l";
            string usage = "Drink it and have a good time!";
            int price1 = 30;

            //Act
            var soda1 = new Soda(price1, name1, info1);

            //Assert
            Assert.Equal(name1, soda1.Name);
            Assert.Equal(info1, soda1.Info);
            Assert.Equal(usage, soda1.UsageDescription);
            Assert.Equal(price1, soda1.Price);

        }

        [Fact]
        public void NormalConstructorCheckAllMethodsTest()
        {
            //Arrange
            string name = "Fanta";
            string info = "0.5 l";
            string usage = "Drink it and have a good time!";
            int price = 18;

            //Act
            var soda = new Soda(price, name, info);
            string examineString = soda.Examine();
            string usageString = soda.Use();

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
            string usage = "Drink it and have a good time!";
            string ExpectedInfo = "Information unknown";
            int price = 20;

            //Act
            var soda1 = new Soda(price, null, null);

            //Assert
            Assert.Equal(ExpectedName, soda1.Name);
            Assert.Equal(ExpectedInfo, soda1.Info);
            Assert.Equal(usage, soda1.UsageDescription); //since this is set in constructor
        }

        [Fact]
        public void ConstructorWithEmptyStringsTest()
        {
            //Arrange
            string ExpectedName = "Unknown name";
            string usage = "Drink it and have a good time!";
            string ExpectedInfo = "Information unknown";
            int price = 20;

            //Act
            var soda1 = new Soda(price, "", "");

            //Assert
            Assert.Equal(ExpectedName, soda1.Name);
            Assert.Equal(ExpectedInfo, soda1.Info);
            Assert.Equal(usage, soda1.UsageDescription); //since this is set in constructor
        }

        [Fact]
        public void ConstructorWithWrongPriceTest()
        {
            //Arrange
            int expectedPrice = 20;
            int price = -20;

            //Act
            var soda1 = new Soda(price, "", "");

            //Assert
            Assert.Equal(expectedPrice, soda1.Price);
        }

        [Fact]
        public void PropertyNameTest()
        {
            //Arrange
            string name = "Sprite";
            string expectedNullName = "Unknown name";
            int price = 20;

            //Act
            var soda = new Soda(price, name, "");
            string initialName = soda.Name;

            soda.Name = null;
            string nullName = soda.Name;

            soda.Name = "";
            string emptyName = soda.Name;

            soda.Name = name;
            string changedName = soda.Name;

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
            string info = "soda 5 cl";
            string expectedNullInfo = "Information unknown";
            int price = 20;

            //Act
            var soda = new Soda(price, "Pepsi", info);
            string initialInfo = soda.Info;

            soda.Info = null;
            string nullName = soda.Info;

            soda.Info = "";
            string emptyName = soda.Info;

            soda.Info = info;
            string changedName = soda.Info;

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
            string name = "Sockerdricka";
            string info = "";
            int price = 10;
            int expectedNegativePrice = 20;
            int negativePrice = -20;
            var soda1 = new Soda(price, name, info);

            //Act

            int initialPrice = soda1.Price;

            //set it to a negative price

            soda1.Price = negativePrice;
            int receivedNegativePrice = soda1.Price;

            //set price to 0
            soda1.Price = 0;
            int zeroPrice = soda1.Price;

            //try normal update of price
            soda1.Price = price;
            int updatedPrice = soda1.Price;

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
            string info = "Ramlösa";
            string expectedNullUsageDescription = "Unknown usage";
            string initialDefaultDescription = "Drink it and have a good time!";
            int price = 20;

            var soda = new Soda(price, info, info);
            //Act

            string initialDescr = soda.UsageDescription; //what is set by constructor

            soda.UsageDescription = null;
            string nullDescr = soda.UsageDescription;

            soda.UsageDescription = "";
            string emptyDescr = soda.UsageDescription;

            soda.UsageDescription = info;
            string changedDescr = soda.UsageDescription;

            string usageDescriptionFromUse = soda.Use();

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
            var soda = new Soda(900, "Loka", "Info");
            string expectedInitialUsage = "Drink it and have a good time!";
            string expectedChangedUsage = "Updated usage";

            //Act
            string receivedIntialUsage = soda.Use();
            soda.UsageDescription = expectedChangedUsage;


            //Assert
            Assert.Equal(expectedInitialUsage, receivedIntialUsage);
            Assert.Equal(expectedChangedUsage, soda.Use());
        }

        [Fact]
        public void MethodExamineTest()
        {
            //Arrange
            var soda1 = new Soda(100, "Amazing Coke", "Made in USA");
            string expectedExaminationContent1 = "Amazing Coke \t\t100 kr. \tMade in USA";

            //Act
            string actualUsage = soda1.Examine();

            //Assert
            Assert.Equal(expectedExaminationContent1, actualUsage);
        }
    }
}
