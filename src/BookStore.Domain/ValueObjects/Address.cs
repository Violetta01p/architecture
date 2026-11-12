namespace BookStore.Domain.ValueObjects;

/// <summary>
/// Value Object — адреса доставки.
/// Не має власного ідентифікатора, порівнюється за значенням полів.
/// </summary>
public class Address
{
    public string City { get; }
    public string Street { get; }
    public string Building { get; }

    public Address(string city, string street, string building)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("Місто не може бути порожнім.", nameof(city));
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Вулиця не може бути порожньою.", nameof(street));
        if (string.IsNullOrWhiteSpace(building))
            throw new ArgumentException("Номер будинку не може бути порожнім.", nameof(building));

        City = city;
        Street = street;
        Building = building;
    }

    // Value Object порівнюється за значенням
    public override bool Equals(object? obj)
    {
        if (obj is not Address other) return false;
        return City == other.City && Street == other.Street && Building == other.Building;
    }

    public override int GetHashCode() => HashCode.Combine(City, Street, Building);
}
