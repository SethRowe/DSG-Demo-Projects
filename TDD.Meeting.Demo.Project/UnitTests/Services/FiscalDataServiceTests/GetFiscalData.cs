using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ExpressionTesting;
using Moq;
using UnitTests.Helpers;
using Xunit;

namespace UnitTests.Services.FiscalDataServiceTests
{
    public class GetFiscalData
    {   
        [Fact]
        public async void ShouldCallTheRepositorysGetItemsMethod()
        {
            // Arrange
            var repository = new Mock<IFiscalDataRepository>();

            repository
                .Setup(x => x.GetItems(It.IsAny<Expression<Func<IFiscalData, bool>>>()))
                .Returns(Task.FromResult((IEnumerable<IFiscalData>) new List<IFiscalData>()))
                .Verifiable();

            // Act
            var service = new FiscalDataService(repository.Object, null);
            await service.GetFiscalData(DateTime.Today);

            // Assert
            repository.Verify();
        }
        
        [Fact]
        public async void ShouldReturnTheFiscalDataObjectFromTheRepository()
        {
            // Arrange
            var expected = new FiscalData();
            var repository = new Mock<IFiscalDataRepository>();

            repository
                .Setup(x => x.GetItems(It.IsAny<Expression<Func<IFiscalData, bool>>>()))
                .Returns(Task.FromResult((IEnumerable<IFiscalData>)new[] { expected }));

            // Act
            var service = new FiscalDataService(repository.Object, null);
            var actual = await service.GetFiscalData(DateTime.Today);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void Option1____WhenGivenADateTimeOtherThanMinValue_ShouldPassItToRepository()
        {
            /* Option 1
             ================
             
             To inspect the expression passed in, we'll use a Callback in Moq to interrogate the parameter sent to
             the Repository's GetItems method.
             
             Rather than parse the Expression Tree, we'll compile it and pass in  some values that would prove it's 
             doing what we expect.
             
             I'm not a big fan of this, since we're "blackbox" testing the Expression, which works, but  isn't ideal.
             
             Also, this test is now HUGE in comparison to other tests.
            */
            
            // Arrange
            
            // Note: Rather than using just "DateTime.Today", I'm randomizing the date. This way, if the SUT has
            // "DateTime.Today" hardcoded, the test will fail.
            var rando = new Random().Next(1, 100);
            var input = DateTime.Today.AddDays(rando);
            var repository = new Mock<IFiscalDataRepository>();

            repository
                .Setup(x => x.GetItems(It.IsAny<Expression<Func<IFiscalData, bool>>>()))
                .Returns(Task.FromResult((IEnumerable<IFiscalData>) new IFiscalData[] { }))
                .Callback<Expression<Func<IFiscalData, bool>>>(expr =>
                {
                    // Assert
                    var compiledExpr = expr.Compile();

                    var matchingDate = new FiscalData {Date = input };
                    var nonMatchingDate1 = new FiscalData {Date = input.AddDays(1)};
                    var nonMatchingDate2 = new FiscalData {Date = input.AddDays(-1)};

                    var matchingResult = compiledExpr(matchingDate);
                    var nonMatchingResult1 = compiledExpr(nonMatchingDate1);
                    var nonMatchingResult2 = compiledExpr(nonMatchingDate2);

                    Assert.True(matchingResult);
                    Assert.False(nonMatchingResult1);
                    Assert.False(nonMatchingResult2);
                });
            
            // Act
            var service = new FiscalDataService(repository.Object, null);
            await service.GetFiscalData(input);
        }

        [Fact]
        public async void Option2____WhenGivenADateTimeOtherThanMinValue_ShouldPassItToRepository()
        {
            /* Option 2
             ================
             
             To inspect the expression passed in, we'll use a Callback in Moq to interrogate the 
             parameter sent to the Repository's GetItems method.
             
             If we know (and we should) that the Expression used will be a BinaryExpression with the
             variable on the Right side, we can grab, then compile, that piece and compare its value
             to our input.
             
             This method is not perfect either, as it's convoluted to write - especially before coding
             the class - and I can see annoying maintenance issues with it. That said, it probably is a
             better test than Option1, as its "whitebox" nature helps reduce false positives. 
         
            */
         
            // Arrange
            var input = DateTime.Today.AddDays(new Random().Next(1, 100));
            var repository = new Mock<IFiscalDataRepository>();

            repository
                .Setup(x => x.GetItems(It.IsAny<Expression<Func<IFiscalData, bool>>>()))
                .Returns(Task.FromResult((IEnumerable<IFiscalData>) new IFiscalData[] { }))
                .Callback<Expression<Func<IFiscalData, bool>>>(expr =>
                {
                    // Assert
                    var memberExpr = (expr.Body as BinaryExpression)?.Right as MemberExpression;

                    // Using Assert.True(...) as xUnit doesn't support messages on Assert.NotNull(...)
                    Assert.True(memberExpr != null, "Unable to convert expression into usable form");
                    
                    var exprValue = memberExpr.GetValue();

                    Assert.Equal(input, exprValue);
                });
            
            // Act
            var service = new FiscalDataService(repository.Object, null);
            await service.GetFiscalData(input);
        }

        [Fact]
        public async void WhenGivenAMinValueForDateTime_ShouldPassDateTimeTodayToRepository()
        {
            // Arrange
            var repository = new Mock<IFiscalDataRepository>();

            repository
                .Setup(x => x.GetItems(It.IsAny<Expression<Func<IFiscalData, bool>>>()))
                .Returns(Task.FromResult((IEnumerable<IFiscalData>) new IFiscalData[] { }))
                .Callback<Expression<Func<IFiscalData, bool>>>(expr =>
                {
                    // Assert
                    var memberExpr = (expr.Body as BinaryExpression)?.Right as MemberExpression;

                    // Using Assert.True(...) as xUnit doesn't support messages on Assert.NotNull(...)
                    Assert.True(memberExpr != null, "Unable to convert expression into usable form");
                    
                    var exprValue = memberExpr.GetValue();

                    Assert.Equal(DateTime.Today, exprValue);
                });
            
            // Act
            await new FiscalDataService(repository.Object, null).GetFiscalData(DateTime.MinValue);
        }

        [Fact]
        public async void WhenNullReturnedFromRepository_ShouldThrowNoDataFoundException()
        {
            // Arrange
            var repository = new Mock<IFiscalDataRepository>();

            repository
                .Setup(x => x.GetItems(It.IsAny<Expression<Func<IFiscalData, bool>>>()))
                .Returns(Task.FromResult((IEnumerable<IFiscalData>)null));
            
            // Act + Assert
            var service = new FiscalDataService(repository.Object, null);
            await Assert.ThrowsAsync<NoDataFoundException>(
                async () => { await service.GetFiscalData(DateTime.Now); }
            );
        }
    }
}