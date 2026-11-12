namespace BookStore.Domain.ValueObjects;

/// <summary>
/// Value Object — грошова сума.
/// Інкапсулює значення та валюту.
/// </summary>
public class Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency = "UAH")
    {
        if (amount < 0)
            throw new ArgumentException("Сума не може бути від'ємною.", nameof(amount));
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Валюта не може бути порожньою.", nameof(currency));

        Amount = amount;
        Currency = currency;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Money other) return false;
        return Amount == other.Amount && Currency == other.Currency;
    }

    public override int GetHashCode() => HashCode.Combine(Amount, Currency);

    public override string ToString() => $"{Amount} {Currency}";
}
