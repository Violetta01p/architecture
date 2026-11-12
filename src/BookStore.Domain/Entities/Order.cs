using BookStore.Domain.Enums;
using BookStore.Domain.ValueObjects;

namespace BookStore.Domain.Entities;

/// <summary>
/// Агрегат (Aggregate Root) — Замовлення.
/// Належить до Bounded Context "Ordering" (управління замовленнями).
/// Є коренем агрегату: керує колекцією OrderItem.
/// </summary>
public class Order
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public OrderStatus Status { get; private set; }
    public Address DeliveryAddress { get; private set; }

    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public Order(Guid id, Guid customerId, Address deliveryAddress)
    {
        Id = id;
        CustomerId = customerId;
        DeliveryAddress = deliveryAddress;
        CreatedAt = DateTime.UtcNow;
        Status = OrderStatus.Created;
    }

    /// <summary>
    /// Бізнес-метод: додати позицію до замовлення.
    /// </summary>
    public void AddItem(Guid bookId, string bookTitle, int quantity, Money unitPrice)
    {
        if (Status != OrderStatus.Created)
            throw new InvalidOperationException("Не можна змінювати підтверджене замовлення.");

        var item = new OrderItem(Guid.NewGuid(), bookId, bookTitle, quantity, unitPrice);
        _items.Add(item);
    }

    /// <summary>
    /// Бізнес-метод: обчислити загальну вартість замовлення.
    /// </summary>
    public decimal GetTotalPrice()
    {
        return _items.Sum(item => item.GetTotalPrice());
    }

    /// <summary>
    /// Бізнес-метод: підтвердити замовлення.
    /// </summary>
    public void Confirm()
    {
        if (Status != OrderStatus.Created)
            throw new InvalidOperationException("Замовлення можна підтвердити лише зі статусу Created.");
        if (!_items.Any())
            throw new InvalidOperationException("Не можна підтвердити порожнє замовлення.");

        Status = OrderStatus.Confirmed;
    }

    /// <summary>
    /// Бізнес-метод: скасувати замовлення.
    /// </summary>
    public void Cancel()
    {
        if (Status == OrderStatus.Delivered)
            throw new InvalidOperationException("Не можна скасувати вже доставлене замовлення.");

        Status = OrderStatus.Cancelled;
    }
}
