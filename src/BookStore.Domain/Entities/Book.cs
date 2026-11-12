using BookStore.Domain.ValueObjects;

namespace BookStore.Domain.Entities;

/// <summary>
/// Сутність (Entity) — Книга.
/// Має унікальний ідентифікатор (Id).
/// Належить до Bounded Context "Catalog" (каталог товарів).
/// </summary>
public class Book
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public string ISBN { get; private set; }
    public Money Price { get; private set; }
    public int StockQuantity { get; private set; }

    // Конструктор з валідацією (бізнес-логіка в доменному шарі)
    public Book(Guid id, string title, string author, string isbn, Money price, int stockQuantity)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Назва книги не може бути порожньою.", nameof(title));
        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("Автор не може бути порожнім.", nameof(author));
        if (stockQuantity < 0)
            throw new ArgumentException("Кількість на складі не може бути від'ємною.", nameof(stockQuantity));

        Id = id;
        Title = title;
        Author = author;
        ISBN = isbn;
        Price = price;
        StockQuantity = stockQuantity;
    }

    /// <summary>
    /// Бізнес-метод: зменшити кількість на складі при замовленні.
    /// </summary>
    public void DecreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Кількість має бути більше нуля.");
        if (StockQuantity < quantity)
            throw new InvalidOperationException("Недостатньо книг на складі.");

        StockQuantity -= quantity;
    }

    /// <summary>
    /// Бізнес-метод: поповнити склад.
    /// </summary>
    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Кількість має бути більше нуля.");

        StockQuantity += quantity;
    }
}
