using BookStore.Domain.Entities;

namespace BookStore.Domain.Interfaces;

/// <summary>
/// Інтерфейс репозиторію для книг.
/// Визначений у доменному шарі — реалізація буде в Infrastructure.
/// Це забезпечує інверсію залежностей (Dependency Inversion Principle).
/// </summary>
public interface IBookRepository
{
    Task<Book?> GetByIdAsync(Guid id);
    Task<IEnumerable<Book>> GetAllAsync();
    Task AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(Guid id);
}
