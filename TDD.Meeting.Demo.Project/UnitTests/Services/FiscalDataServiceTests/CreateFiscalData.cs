using System;
using System.Threading.Tasks;
using ExpressionTesting;
using Moq;
using Xunit;

namespace UnitTests.Services.FiscalDataServiceTests
{
    public class CreateFiscalData
    {
        [Fact]
        public async Task WhenProvidedANullFiscalData_ShouldThrowArgumentNullException()
        {
            // Arrange, Act, Assert
            // Note: first parameter (paramName) is optional, but useful for Argument and ArgumentNull exceptions
            await Assert.ThrowsAsync<ArgumentNullException>("fiscalData", async () =>
            {
                await new FiscalDataService(null, null).CreateFiscalData(null); 
            });
        }

        [Fact]
        public async Task WhenProvidedANonNullFiscalData_ShouldCallValidationHelper()
        {
            // Arrange
            var validator = new Mock<IValidator<IFiscalData>>();

            validator
                .Setup(x => x.IsValid(It.IsAny<IFiscalData>()))
                .Returns(false)
                .Verifiable();
            
            // Act
            await new FiscalDataService(null, validator.Object).CreateFiscalData(new FiscalData());
            
            // Assert
            validator.VerifyAll();
        }
        
        [Fact]
        public async Task WhenValidationFails_ShouldReturnNegativeOne()
        {
            // Arrange
            var validator = new Mock<IValidator<IFiscalData>>();
            
            validator
                .Setup(x => x.IsValid(It.IsAny<IFiscalData>()))
                .Returns(false);
            
            // Act
            var service = new FiscalDataService(null, validator.Object);
            var returnValue = await service.CreateFiscalData(new FiscalData());
            
            // Assert
            Assert.Equal(-1, returnValue);
        }
        
        [Fact]
        public async Task WhenValidationFails_ShouldNotCallRepositoryCreate()
        {
            // Arrange
            var validator = new Mock<IValidator<IFiscalData>>();
            
            // Strict, combined with no setups,  will make sure our test fails if ANY method
            // is called on the repository mock
            var repository = new Mock<IFiscalDataRepository>(MockBehavior.Strict);

            validator
                .Setup(x => x.IsValid(It.IsAny<IFiscalData>()))
                .Returns(false);
            
            // Act
            await new FiscalDataService(repository.Object, validator.Object).CreateFiscalData(new FiscalData());
            
            // Assert
           repository.VerifyAll();
        }
        
        [Fact]
        public async Task WhenValidationPasses_ShouldCallRepositoryCreate()
        {
            // Arrange
            var validator = new Mock<IValidator<IFiscalData>>();
            var repository = new Mock<IFiscalDataRepository>();

            validator
                .Setup(x => x.IsValid(It.IsAny<IFiscalData>()))
                .Returns(true);

            repository
                .Setup(x => x.CreateItem(It.IsAny<IFiscalData>()))
                .Returns(Task.FromResult(0))
                .Verifiable();
            
            // Act
            await new FiscalDataService(repository.Object, validator.Object).CreateFiscalData(new FiscalData());
            
            // Assert
            repository.Verify();
        }
        
        [Fact]
        public async Task WhenCallingRepositoryCreate_ShouldPassProvidedFiscalData()
        {
            // Arrange
            var input = new FiscalData();
            
            var validator = new Mock<IValidator<IFiscalData>>();
            var repository = new Mock<IFiscalDataRepository>();

            validator
                .Setup(x => x.IsValid(It.IsAny<IFiscalData>()))
                .Returns(true);

            // Note: This doesn't use It.IsAny<>, but the object reference instead.
            // This will cause the .Verify() to fail if any other IFiscalData (or null) is passed
            repository
                .Setup(x => x.CreateItem(input))
                .Returns(Task.FromResult(0))
                .Verifiable();
            
            // Act
            await new FiscalDataService(repository.Object, validator.Object).CreateFiscalData(input);
            
            // Assert
            repository.Verify();
        }
        
        [Fact]
        public async Task WhenCallingRepositoryCreate_ShouldReturnPrimaryKeyReturnedByRepository()
        {
            // Arrange
            var validator = new Mock<IValidator<IFiscalData>>();
            var repository = new Mock<IFiscalDataRepository>();

            validator
                .Setup(x => x.IsValid(It.IsAny<IFiscalData>()))
                .Returns(true);

            var rando = new Random().Next(1, 1000);

            repository
                .Setup(x => x.CreateItem(It.IsAny<IFiscalData>()))
                .Returns(Task.FromResult(rando));
            
            // Act
            var service = new FiscalDataService(repository.Object, validator.Object);
            var returnValue = await service.CreateFiscalData(new FiscalData());
            
            // Assert
            Assert.Equal(rando, returnValue);
        }
    }
}