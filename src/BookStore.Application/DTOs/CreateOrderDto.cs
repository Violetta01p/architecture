namespace BookStore.Application.DTOs;

/// <summary>
/// DTO для створення замовлення.
/// </summary>
public class CreateOrderDto
{
    public Guid CustomerId { get; set; }
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Building { get; set; } = string.Empty;
    public List<OrderItemDto> Items { get; set; } = new();
}

public class OrderItemDto
{
    public Guid BookId { get; set; }
    public int Quantity { get; set; }
}
