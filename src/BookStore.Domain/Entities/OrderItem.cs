using BookStore.Domain.ValueObjects;

namespace BookStore.Domain.Entities;

/// <summary>
/// Сутність (Entity) — Позиція замовлення.
/// Містить інформацію про конкретну книгу в замовленні.
/// </summary>
public class OrderItem
{
    public Guid Id { get; private set; }
    public Guid BookId { get; private set; }
    public string BookTitle { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; }

    public OrderItem(Guid id, Guid bookId, string bookTitle, int quantity, Money unitPrice)
    {
        if (quantity <= 0)
            throw new ArgumentException("Кількість має бути більше нуля.", nameof(quantity));

        Id = id;
        BookId = bookId;
        BookTitle = bookTitle;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    /// <summary>
    /// Обчислення загальної вартості позиції.
    /// </summary>
    public decimal GetTotalPrice() => UnitPrice.Amount * Quantity;
}
