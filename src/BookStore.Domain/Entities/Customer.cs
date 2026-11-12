namespace BookStore.Domain.Entities;

/// <summary>
/// Сутність (Entity) — Клієнт.
/// Належить до Bounded Context "Customer" (управління клієнтами).
/// </summary>
public class Customer
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }

    public Customer(Guid id, string fullName, string email, string phone)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("ПІБ не може бути порожнім.", nameof(fullName));
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email не може бути порожнім.", nameof(email));

        Id = id;
        FullName = fullName;
        Email = email;
        Phone = phone;
    }
}
