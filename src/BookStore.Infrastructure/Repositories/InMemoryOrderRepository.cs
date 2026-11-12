using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;

namespace BookStore.Infrastructure.Repositories;

/// <summary>
/// In-Memory реалізація репозиторію замовлень.
/// </summary>
public class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Order> _orders = new();

    public Task<Order?> GetByIdAsync(Guid id)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        return Task.FromResult(order);
    }

    public Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customerId)
    {
        var orders = _orders.Where(o => o.CustomerId == customerId);
        return Task.FromResult<IEnumerable<Order>>(orders.ToList());
    }

    public Task AddAsync(Order order)
    {
        _orders.Add(order);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Order order)
    {
        return Task.CompletedTask;
    }
}
