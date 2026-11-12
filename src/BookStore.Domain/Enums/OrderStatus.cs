namespace BookStore.Domain.Enums;

/// <summary>
/// Статус замовлення
/// </summary>
public enum OrderStatus
{
    Created = 0,
    Confirmed = 1,
    Shipped = 2,
    Delivered = 3,
    Cancelled = 4
}
