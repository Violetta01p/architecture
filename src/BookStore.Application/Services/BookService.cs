using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Domain.ValueObjects;

namespace BookStore.Application.Services;

/// <summary>
/// Прикладний сервіс для роботи з книгами.
/// Координує виклики до доменного шару та репозиторіїв.
/// НЕ містить бізнес-логіки — лише оркестрацію.
/// </summary>
public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookDto?> GetByIdAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null) return null;

        return MapToDto(book);
    }

    public async Task<IEnumerable<BookDto>> GetAllAsync()
    {
        var books = await _bookRepository.GetAllAsync();
        return books.Select(MapToDto);
    }

    public async Task<BookDto> CreateAsync(CreateBookDto dto)
    {
        var price = new Money(dto.Price);
        var book = new Book(Guid.NewGuid(), dto.Title, dto.Author, dto.ISBN, price, dto.StockQuantity);

        await _bookRepository.AddAsync(book);
        return MapToDto(book);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _bookRepository.DeleteAsync(id);
    }

    // Маппінг доменної сутності у DTO
    private static BookDto MapToDto(Book book) => new()
    {
        Id = book.Id,
        Title = book.Title,
        Author = book.Author,
        ISBN = book.ISBN,
        Price = book.Price.Amount,
        StockQuantity = book.StockQuantity
    };
}
