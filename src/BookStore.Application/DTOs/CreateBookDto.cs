namespace BookStore.Application.DTOs;

/// <summary>
/// DTO для створення книги (вхідні дані від користувача).
/// </summary>
public class CreateBookDto
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
