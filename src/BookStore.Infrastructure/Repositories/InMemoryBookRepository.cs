using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;

namespace BookStore.Infrastructure.Repositories;

/// <summary>
/// In-Memory реалізація репозиторію книг.
/// Реалізує інтерфейс з доменного шару — це і є інверсія залежностей.
/// В реальному проєкті тут буде Entity Framework або інший ORM.
/// </summary>
public class InMemoryBookRepository : IBookRepository
{
    private readonly List<Book> _books = new();

    public Task<Book?> GetByIdAsync(Guid id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        return Task.FromResult(book);
    }

    public Task<IEnumerable<Book>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Book>>(_books.ToList());
    }

    public Task AddAsync(Book book)
    {
        _books.Add(book);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Book book)
    {
        // In-Memory: об'єкт вже оновлений за посиланням
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book != null) _books.Remove(book);
        return Task.CompletedTask;
    }
}
