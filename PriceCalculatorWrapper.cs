using System.Collections.Generic;

public class PriceCalculatorWrapper : IPriceCalculator
{
    private readonly IPriceCalculator _calculator;

    public PriceCalculatorWrapper(IPriceCalculator calculator = null)
    {
        _calculator = calculator ?? new PriceCalculator();
    }

    public decimal CalculateTotalPrice(List<decimal> prices, decimal discountPercentage)
    {
        return _calculator.CalculateTotalPrice(prices, discountPercentage);
    }
} 