using BookStore.Application.DTOs;

namespace BookStore.Application.Interfaces;

/// <summary>
/// Інтерфейс прикладного сервісу для роботи з книгами.
/// Визначає use cases (сценарії використання).
/// </summary>
public interface IBookService
{
    Task<BookDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<BookDto>> GetAllAsync();
    Task<BookDto> CreateAsync(CreateBookDto dto);
    Task DeleteAsync(Guid id);
}
