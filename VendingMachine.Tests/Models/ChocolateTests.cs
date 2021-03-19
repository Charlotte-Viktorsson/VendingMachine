using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using VendingMachine.Models;

namespace VendingMachine.Tests.Models
{

    public class ChocolateTests
    {

        [Fact]
        public void NormalConstructorCheckAllFieldsTest()
        {
            //Arrange
            string name1 = "Marabou Mjölkchoklad";
            string info1 = "chokladkaka 100g";
            string usage = "Eat it and enjoy!";
            int price1 = 20;

            //Act
            Chocolate choc1 = new Chocolate(price1, name1, info1);

            //Assert
            Assert.Equal(name1, choc1.Name);
            Assert.Equal(info1, choc1.Info);
            Assert.Equal(usage, choc1.UsageDescription);
            Assert.Equal(price1, choc1.Price);
        }


        [Fact]
        public void NormalConstructorCheckAllMethodsTest()
        {
            //Arrange
            string name = "Marabou Nötchoklad";
            string info = "chokladkaka 100g";
            string usage = "Eat it and enjoy!";
            int price = 20;

            //Act
            var choc = new Chocolate(price, name, info);
            string examineString = choc.Examine();
            string usageString = choc.Use();

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
            string usage = "Eat it and enjoy!";
            string ExpectedInfo = "Information unknown";
            int price = 20;

            //Act
            var choc1 = new Chocolate(price, null, null);

            //Assert
            Assert.Equal(ExpectedName, choc1.Name);
            Assert.Equal(ExpectedInfo, choc1.Info);
            Assert.Equal(usage, choc1.UsageDescription); //since this is set in constructor
        }

        [Fact]
        public void ConstructorWithEmptyStringsTest()
        {
            //Arrange
            string ExpectedName = "Unknown name";
            string usage = "Eat it and enjoy!";
            string ExpectedInfo = "Information unknown";
            int price = 20;

            //Act
            var choc1 = new Chocolate(price, "", "");

            //Assert
            Assert.Equal(ExpectedName, choc1.Name);
            Assert.Equal(ExpectedInfo, choc1.Info);
            Assert.Equal(usage, choc1.UsageDescription); //since this is set in constructor
        }

        [Fact]
        public void ConstructorWithWrongPriceTest()
        {
            //Arrange
            int expectedPrice = 200;
            int price = -200;

            //Act
            var choc1 = new Chocolate(price, "", "");

            //Assert
            Assert.Equal(expectedPrice, choc1.Price);
        }

        [Fact]
        public void PropertyNameTest()
        {
            //Arrange
            string name = "Marabou Nötchoklad";
            string expectedNullName = "Unknown name";
            int price = 20;

            //Act
            var choc = new Chocolate(price, name, "");
            string initialName = choc.Name;

            choc.Name = null;
            string nullName = choc.Name;

            choc.Name = "";
            string emptyName = choc.Name;

            choc.Name = name;
            string changedName = choc.Name;

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
            string info = "choklad 100 g";
            string expectedNullInfo = "Information unknown";
            int price = 20;

            //Act
            var choc = new Chocolate(price, "", info);
            string initialInfo = choc.Info;

            choc.Info = null;
            string nullName = choc.Info;

            choc.Info = "";
            string emptyName = choc.Info;

            choc.Info = info;
            string changedName = choc.Info;

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
            string name = "Marabou Nötchoklad";
            string info = "chokladkaka 100g";
            int price = 20;

            var choc1 = new Chocolate(price, name, info);

            //Act

            int initialPrice = choc1.Price;

            //try to set it to a negative price
            int negativePrice = -100;
            choc1.Price = negativePrice;
            int expectedNegativePrice = 100;
            int receivedNegativePrice = choc1.Price;

            //try set price to 0
            choc1.Price = 0;
            int zeroPrice = choc1.Price;

            //try normal update of price
            choc1.Price = price;
            int updatedPrice = choc1.Price;

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
            string info = "choklad";
            string expectedNullUsageDescription = "Unknown usage";
            string initialDefaultDescription = "Eat it and enjoy!";
            int price = 20;

            var choc = new Chocolate(price, info, info);
            //Act

            string initialDescr = choc.UsageDescription; //what is set by constructor

            choc.UsageDescription = null;
            string nullDescr = choc.UsageDescription;

            choc.UsageDescription = "";
            string emptyDescr = choc.UsageDescription;

            choc.UsageDescription = info;
            string changedDescr = choc.UsageDescription;

            string usageDescriptionFromUse = choc.Use();

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
            var choc = new Chocolate(900, "Mine", "Info");
            string expectedInitialUsage = "Eat it and enjoy!";
            string expectedChangedUsage = "Updated usage";

            //Act
            string receivedIntialUsage = choc.Use();
            choc.UsageDescription = expectedChangedUsage;


            //Assert
            Assert.Equal(expectedInitialUsage, receivedIntialUsage);
            Assert.Equal(expectedChangedUsage, choc.Use());
        }

        [Fact]
        public void MethodExamineTest()
        {
            //this method is overrided by Chokolate
            //Arrange
            Chocolate choc1 = new Chocolate(1, "Short name", "T");
            var chocLongerName = new Chocolate(100, "Marabous best chocolate", "Good chocolate");
            string expectedExaminationContent1 = "Short name\t1 kr. \tT";
            string expectedLongerExaminationContent = "Marabous best chocolate100 kr. \tGood chocolate";


            //Act
            string actualChocUsage = choc1.Examine();
            string actualLongerUsage = chocLongerName.Examine();

            //Assert
            Assert.Equal(expectedExaminationContent1, actualChocUsage);

            Assert.Equal(expectedLongerExaminationContent, actualLongerUsage);
        }

    }
}
