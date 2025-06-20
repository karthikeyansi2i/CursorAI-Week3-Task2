using System;
using System.Collections.Generic;
using Xunit;
using Moq;

public class PriceCalculatorTests
{
    // 1. Basic Functionality
    [Fact]
    public void CalculateTotalPrice_MultiplePrices_ValidDiscount()
    {
        var calc = new PriceCalculator();
        var prices = new List<decimal> { 10, 20, 30 };
        var result = calc.CalculateTotalPrice(prices, 10);
        Assert.Equal(54, result);
    }

    // 2. No Discount
    [Fact]
    public void CalculateTotalPrice_NoDiscount()
    {
        var calc = new PriceCalculator();
        var prices = new List<decimal> { 10, 20, 30 };
        var result = calc.CalculateTotalPrice(prices, 0);
        Assert.Equal(60, result);
    }

    // 3. Full Discount
    [Fact]
    public void CalculateTotalPrice_FullDiscount()
    {
        var calc = new PriceCalculator();
        var prices = new List<decimal> { 10, 20, 30 };
        var result = calc.CalculateTotalPrice(prices, 100);
        Assert.Equal(0, result);
    }

    // 4. Empty Price List
    [Fact]
    public void CalculateTotalPrice_EmptyList()
    {
        var calc = new PriceCalculator();
        var prices = new List<decimal>();
        var result = calc.CalculateTotalPrice(prices, 10);
        Assert.Equal(0, result);
    }

    // 5. Single Item
    [Fact]
    public void CalculateTotalPrice_SingleItem()
    {
        var calc = new PriceCalculator();
        var prices = new List<decimal> { 50 };
        var result = calc.CalculateTotalPrice(prices, 20);
        Assert.Equal(40, result);
    }

    // 6. Negative Prices
    [Fact]
    public void CalculateTotalPrice_NegativePrices()
    {
        var calc = new PriceCalculator();
        var prices = new List<decimal> { 10, -5, 20 };
        var result = calc.CalculateTotalPrice(prices, 10);
        Assert.Equal(22.5m, result);
    }

    // 7. Negative Discount
    [Fact]
    public void CalculateTotalPrice_NegativeDiscount()
    {
        var calc = new PriceCalculator();
        var prices = new List<decimal> { 10, 20 };
        var result = calc.CalculateTotalPrice(prices, -10);
        Assert.Equal(33, result);
    }

    // 8. Discount Greater Than 100%
    [Fact]
    public void CalculateTotalPrice_DiscountGreaterThan100()
    {
        var calc = new PriceCalculator();
        var prices = new List<decimal> { 10, 20 };
        var result = calc.CalculateTotalPrice(prices, 150);
        Assert.Equal(-15, result);
    }

    // 9. Decimal Prices and Discount
    [Fact]
    public void CalculateTotalPrice_DecimalPricesAndDiscount()
    {
        var calc = new PriceCalculator();
        var prices = new List<decimal> { 10.5m, 20.25m };
        var result = calc.CalculateTotalPrice(prices, 12.5m);
        Assert.Equal(26.90625m, result);
    }

    // 10. Large Numbers
    [Fact]
    public void CalculateTotalPrice_LargeNumbers()
    {
        var calc = new PriceCalculator();
        var prices = new List<decimal> { 1000000, 2000000 };
        var result = calc.CalculateTotalPrice(prices, 50);
        Assert.Equal(1500000, result);
    }

    // 11. Wrapper uses legacy by default
    [Fact]
    public void PriceCalculatorWrapper_DefaultsToLegacy()
    {
        var wrapper = new PriceCalculatorWrapper();
        var prices = new List<decimal> { 10, 20, 30 };
        var result = wrapper.CalculateTotalPrice(prices, 10);
        Assert.Equal(54, result);
    }

    // 12. Wrapper uses injected dependency
    [Fact]
    public void PriceCalculatorWrapper_UsesInjectedDependency()
    {
        var mock = new Mock<IPriceCalculator>();
        mock.Setup(x => x.CalculateTotalPrice(It.IsAny<List<decimal>>(), It.IsAny<decimal>())).Returns(42);
        var wrapper = new PriceCalculatorWrapper(mock.Object);
        var prices = new List<decimal> { 1, 2, 3 };
        var result = wrapper.CalculateTotalPrice(prices, 99);
        Assert.Equal(42, result);
        mock.Verify(x => x.CalculateTotalPrice(prices, 99), Times.Once);
    }

    // 13. Null price list throws exception (optional, as legacy does not handle this)
    [Fact]
    public void CalculateTotalPrice_NullList_Throws()
    {
        var calc = new PriceCalculator();
        Assert.Throws<ArgumentNullException>(() => calc.CalculateTotalPrice(null, 10));
    }
} 