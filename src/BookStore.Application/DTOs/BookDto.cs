namespace BookStore.Application.DTOs;

/// <summary>
/// DTO для передачі даних про книгу між шарами.
/// DTO не містить бізнес-логіки.
/// </summary>
public class BookDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
