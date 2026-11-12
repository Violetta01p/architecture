using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Domain.ValueObjects;

namespace BookStore.Application.Services;

/// <summary>
/// Прикладний сервіс для роботи з замовленнями.
/// Координує взаємодію між Bounded Context "Ordering" та "Catalog".
/// </summary>
public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IBookRepository _bookRepository;
    private readonly ICustomerRepository _customerRepository;

    public OrderService(
        IOrderRepository orderRepository,
        IBookRepository bookRepository,
        ICustomerRepository customerRepository)
    {
        _orderRepository = orderRepository;
        _bookRepository = bookRepository;
        _customerRepository = customerRepository;
    }

    public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto)
    {
        // Перевірка існування клієнта
        var customer = await _customerRepository.GetByIdAsync(dto.CustomerId);
        if (customer == null)
            throw new ArgumentException("Клієнта не знайдено.");

        // Створення замовлення (доменна логіка)
        var address = new Address(dto.City, dto.Street, dto.Building);
        var order = new Order(Guid.NewGuid(), dto.CustomerId, address);

        // Додавання позицій (взаємодія з Bounded Context "Catalog")
        foreach (var itemDto in dto.Items)
        {
            var book = await _bookRepository.GetByIdAsync(itemDto.BookId);
            if (book == null)
                throw new ArgumentException($"Книгу з Id={itemDto.BookId} не знайдено.");

            // Бізнес-логіка знаходиться в доменному шарі (в сутності Book)
            book.DecreaseStock(itemDto.Quantity);
            await _bookRepository.UpdateAsync(book);

            order.AddItem(book.Id, book.Title, itemDto.Quantity, book.Price);
        }

        await _orderRepository.AddAsync(order);
        return MapToDto(order);
    }

    public async Task<OrderDto?> GetByIdAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null) return null;

        return MapToDto(order);
    }

    public async Task ConfirmOrderAsync(Guid orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
            throw new ArgumentException("Замовлення не знайдено.");

        // Бізнес-логіка — в доменній сутності Order
        order.Confirm();
        await _orderRepository.UpdateAsync(order);
    }

    public async Task CancelOrderAsync(Guid orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
            throw new ArgumentException("Замовлення не знайдено.");

        order.Cancel();
        await _orderRepository.UpdateAsync(order);
    }

    private static OrderDto MapToDto(Order order) => new()
    {
        Id = order.Id,
        CustomerId = order.CustomerId,
        CreatedAt = order.CreatedAt,
        Status = order.Status.ToString(),
        DeliveryAddress = $"{order.DeliveryAddress.City}, {order.DeliveryAddress.Street}, {order.DeliveryAddress.Building}",
        TotalPrice = order.GetTotalPrice(),
        Items = order.Items.Select(i => new OrderItemResponseDto
        {
            BookTitle = i.BookTitle,
            Quantity = i.Quantity,
            UnitPrice = i.UnitPrice.Amount,
            TotalPrice = i.GetTotalPrice()
        }).ToList()
    };
}
