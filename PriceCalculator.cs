using System;
using System.Collections.Generic;

public class PriceCalculator : IPriceCalculator
{
    public decimal CalculateTotalPrice(List<decimal> prices, decimal discountPercentage)
    {
        if (prices == null)
            throw new ArgumentNullException(nameof(prices));

        decimal totalPrice = 0;
        foreach (var price in prices)
        {
            totalPrice += price;
        }
        totalPrice -= totalPrice * (discountPercentage / 100);
        return totalPrice;
    }
} 