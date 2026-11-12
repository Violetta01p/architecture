# Практична робота №1: Чиста архітектура та Domain-Driven Design

## Предметна область: Інтернет-магазин книг (BookStore)

### Архітектура системи
Проєкт реалізовано на платформі **ASP.NET Core (.NET 8)** з дотриманням принципів **Clean Architecture** (Чистої архітектури) та **Domain-Driven Design (DDD)**.

Система розділена на 4 ізольованих шари:
1. **BookStore.Domain** (Доменний шар):
   - **Сутності (Entities)**: `Book`, `Customer`, `OrderItem`.
   - **Агрегат (Aggregate Root)**: `Order` — контролює власні позиції та інваріанти бізнес-правил.
   - **Об'єкти-значення (Value Objects)**: `Address` (місто, вулиця, будинок), `Money` (сума та валюта).
   - **Інтерфейси репозиторіїв**: `IBookRepository`, `IOrderRepository`, `ICustomerRepository`.
   - *Шар не залежить від жодного іншого компонента чи фреймворку.*

2. **BookStore.Application** (Прикладний шар):
   - Сервіси use-case логіки: `BookService`, `OrderService`.
   - DTO об'єкти для передачі даних без витоку доменних моделей.
   - Залежить виключно від `Domain`.

3. **BookStore.Infrastructure** (Інфраструктурний шар):
   - Реалізація інтерфейсів репозиторіїв (`InMemoryBookRepository`, `InMemoryOrderRepository`, `InMemoryCustomerRepository`).
   - Реалізує принцип інверсії залежностей (DIP).

4. **BookStore.WebAPI** (Презентаційний шар):
   - REST API контролери: `BooksController`, `OrdersController`.
   - Налаштування Dependency Injection у `Program.cs`.
   - Документація та тестування через Swagger UI.

### Bounded Contexts
1. **Catalog Context**: каталог книг, ціни, залишки на складі (`Book`, `Money`).
2. **Ordering Context**: замовлення, адреса доставки, життєвий цикл замовлення (`Order`, `OrderItem`, `Address`).
3. **Customer Management Context**: облік покупців (`Customer`).

Взаємодія між контекстами відбувається через ідентифікатори (`BookId`, `CustomerId`).
