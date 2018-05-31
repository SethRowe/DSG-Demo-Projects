using System;
using System.Threading.Tasks;
using DSG.TDD.StarterProject.Api.Controllers;
using DSG.TDD.StarterProject.Managers.Entities;
using DSG.TDD.StarterProject.Managers.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DSG.TDD.StarterProject.UnitTests.Controllers.ProductControllerTests
{
    [TestClass]
    public class Get
    {
        /*
         *
         * when we get an invalid id, then return 400
         * when we get a valid id, then call the manager's get items
         * when we get a valid id, then call the manager's get items, with correct id
         * when manager returns null, then404 notfound
         * when manager return an object, then return a 200 OK result
         * when manager return an object, then return a 200 OK result, with the object 
         * when exception is thrown, then return 500 Internal Server Error
         *
         * ==========================================================
         *  (these two tests were discovered while writing the code)
         * ==========================================================
         *
         * when exception is thrown, the call the logger's log method
         * when exception is thrown, the call the logger's log method, with the exception
         *
         */

        [TestMethod]
        public async Task WhenWeGetAnInvalidId_ThenReturnBadRequest()
        {
            // Arrange
            var invalidId = -278;

            // Act
            var controller = new ProductController(null);
            var result = await controller.Get(invalidId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task WhenWeGetAValidId_ThenCallManagerGetItem()
        {
            // Arrange
            var productManagerMock = new Mock<IProductManager>();

            productManagerMock
                .Setup(x => x.GetItem(It.IsAny<long>()))
                .Returns(Task.FromResult(null as Product));

            // Act
            var controller = new ProductController(productManagerMock.Object);
            await controller.Get(4567);

            // Assert
            productManagerMock.VerifyAll();
        }

        [TestMethod]
        public async Task WhenWeGetAValidId_ThenCallManagerGetItemWithCorrectId()
        {
            // Arrange
            var expectedId = 98798769273;

            var productManagerMock = new Mock<IProductManager>();

            productManagerMock
                .Setup(x => x.GetItem(expectedId))
                .Returns(Task.FromResult(null as Product));

            // Act
            var controller = new ProductController(productManagerMock.Object);
            await controller.Get(expectedId);

            // Assert
            productManagerMock.VerifyAll();
        }

        [TestMethod]
        public async Task WhenManagerReturnsNull_ThenReturnNotFound()
        {
            // Arrange
            var productManagerMock = new Mock<IProductManager>();

            productManagerMock
                .Setup(x => x.GetItem(It.IsAny<long>()))
                .Returns(Task.FromResult(null as Product));

            // Act
            var controller = new ProductController(productManagerMock.Object);
            var result = await controller.Get(4567);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task WhenManagerReturnsAProduct_ThenReturnOK()
        {
            // Arrange
            var productManagerMock = new Mock<IProductManager>();

            productManagerMock
                .Setup(x => x.GetItem(It.IsAny<long>()))
                .Returns(Task.FromResult(new Product()));

            // Act
            var controller = new ProductController(productManagerMock.Object);
            var result = await controller.Get(4567);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

       
        [TestMethod]
        public async Task WhenManagerReturnsAProduct_ThenReturnOKWithTheProduct()
        {
            // Arrange
            var expected = new Product {Name = "hello"};

            var productManagerMock = new Mock<IProductManager>();

            productManagerMock
                .Setup(x => x.GetItem(It.IsAny<long>()))
                .Returns(Task.FromResult(expected));

            // Act
            var controller = new ProductController(productManagerMock.Object);
            dynamic result = await controller.Get(4567);

            // Assert
            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod]
        public async Task WhenAnExceptionIsThrown_ThenReturnStatusCode500()
        {
            // Arrange
            var productManagerMock = new Mock<IProductManager>();

            productManagerMock
                .Setup(x => x.GetItem(It.IsAny<long>()))
                .Returns(Task.FromResult(null as Product))
                .Callback(() => throw new Exception("hello, TDD'ers"));

            // Act
            var controller = new ProductController(productManagerMock.Object);
            var result = (await controller.Get(4567)) as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result, "Result was not a StatusCodeResult");
            Assert.AreEqual(500, result.StatusCode);
        }

        [TestMethod]
        public async Task WhenAnExceptionIsThrown_ThenCallTheLoggersLogMethod()
        {
            throw new NotImplementedException("todo");
        }

        [TestMethod]
        public async Task WhenAnExceptionIsThrown_ThenCallTheLoggersLogMethodWithTheThrownExcpetion()
        {
            throw new NotImplementedException("todo");
        }
    }
}  