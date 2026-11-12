using BookStore.Application.DTOs;

namespace BookStore.Application.Interfaces;

/// <summary>
/// Інтерфейс прикладного сервісу для роботи з замовленнями.
/// </summary>
public interface IOrderService
{
    Task<OrderDto> CreateOrderAsync(CreateOrderDto dto);
    Task<OrderDto?> GetByIdAsync(Guid id);
    Task ConfirmOrderAsync(Guid orderId);
    Task CancelOrderAsync(Guid orderId);
}
