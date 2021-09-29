using System;
using Models;
using Xunit;

namespace Test
{
    public class ModelTest
    {
        [Fact]
        public void CustomerShouldCreate()
        {
            Customer test = new Customer();

            Assert.NotNull(test);
        } 
        [Fact]
        public void CustomerShouldSetValidData()
        {
            //Arrange
            Customer test = new Customer();
            string testName = "test";

            //Act
            test.Name = testName;

            //Assert
            Assert.Equal(testName, test.Name);
        }
        [Fact]
        public void OrderShouldCreate()
        {
            Order test = new Order();

            Assert.NotNull(test);
        } 
        [Fact]
        
        public void OrderhouldSetValidData()
        {
            //Arrange
            Order test = new Order();
            int testId = 1;

            //Act
            test.OrderId = testId;

            //Assert
            Assert.Equal(testId, test.OrderId);
        } 
        [Fact]
        public void LineItemShouldCreate()
        {
            LineItem test = new LineItem();

            Assert.NotNull(test);
        } 
        [Fact]
        public void LineItemhouldSetValidData()
        {
            //Arrange
            LineItem test = new LineItem();
            int testId = 1;

            //Act
            test.OrderID = testId;

            //Assert
            Assert.Equal(testId, test.OrderID);
        } 
        [Fact]
        public void ProductShouldCreate()
        {
            Product test = new Product();

            Assert.NotNull(test);
        }
        [Fact]
        public void ProductShouldSetValidData()
        {
            //Arrange
            Product test = new Product();
            string testName = "test ";

            //Act
            test.Name = testName;

            //Assert
            Assert.Equal(testName, test.Name);
        } 
        [Fact]
        public void StoreFrontShouldCreate()
        {
            StoreFront test = new StoreFront();

            Assert.NotNull(test);
        } 
        [Fact]
        public void StoreFrontShouldSetValidData()
        {
            //Arrange
            StoreFront test = new StoreFront();
            string testName = "test ";

            //Act
            test.Name = testName;

            //Assert
            Assert.Equal(testName, test.Name);
        } 

    }
}
