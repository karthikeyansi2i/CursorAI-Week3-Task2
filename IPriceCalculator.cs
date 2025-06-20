public interface IPriceCalculator
{
    decimal CalculateTotalPrice(List<decimal> prices, decimal discountPercentage);
} 