# CG4 2.0
## Описание проекта
CG4 - это шаблон для создания масштабируемых .NET Web API приложений с поддержкой различных технологий и инфраструктурных компонентов.

### Основные компоненты
- `CG4.Executor` - ядро для выполнения операций, предоставляет базовую инфраструктуру для обработки запросов
- `CG4.DataAccess` - слой доступа к данным, абстракции для работы с различными источниками данных
- `CG4.Extensions` - набор расширений для упрощения работы с основными компонентами
- `CG4.Web` - веб-компоненты, базовые классы для создания API

### Поддерживаемые технологии
- **Базы данных**:
  - `CG4.Impl.Dapper` - быстрый доступ к данным через Dapper (PostgreSQL)
  - `CG4.Impl.EF` - работа с Entity Framework Core
  - `CG4.Impl.MongoDB` - поддержка MongoDB
  - `CG4.Impl.ElasticSearch` - интеграция с ElasticSearch для поиска и аналитики
  - `CG4.Impl.Memory` - in-memory реализация для тестирования

- **Очереди сообщений**:
  - `CG4.Impl.Rabbit` - интеграция с RabbitMQ
  - `CG4.Impl.Kafka` - поддержка Apache Kafka

- **Мониторинг**:
  - `CG4.Impl.Prometheus` - метрики Prometheus для мониторинга

### Тестирование
- Модульные тесты для каждого компонента
- Бенчмарки для тестирования производительности
- Интеграционные тесты для проверки взаимодействия компонентов

## CG4.Executor
### Интерфейс IStory

Интерфейс `IStory` представляет собой основной компонент для реализации бизнес-логики в архитектуре CG4. 
Он используется для создания инкапсулированных операций, которые работают с конкретным контекстом и могут возвращать или не возвращать результат.

### Основные принципы

- `IStory` реализует шаблон проектирования Command Pattern
- Выполняет одну атомарную бизнес-операцию
- Каждая Story должна иметь свой Context
- Поддерживает DI через конструктор

### Как использовать IStory?

#### 1. Определение контекста

Создайте класс контекста, реализующий интерфейс `IResult<T>` или `IResult`:

```csharp
// Контекст с возвращаемым значением
public class CreateAccountStoryContext : IResult<CreatedIdResult>
{
    public AccountCreateDto Data { get; set; }
}

// Контекст без возвращаемого значения
public class DeleteAccountStoryContext : IResult
{
    public int AccountId { get; set; }
}
```

#### 2. Реализация IStory

Создайте класс, реализующий интерфейс `IStory`:

```csharp
// Story с возвращаемым значением
public class CreateAccountStory : IStory<CreateAccountStoryContext, CreatedIdResult>
{
    private readonly ICrudService _crudService;

    public CreateAccountStory(ICrudService crudService)
    {
        _crudService = crudService;
    }

    public async Task<CreatedIdResult> ExecuteAsync(CreateAccountStoryContext context)
    {
        var account = new Account
        {
            Login = context.Data.Login,
            Name = context.Data.Name,
            Password = context.Data.Password,
        };

        var result = await _crudService.CreateAsync(account);

        return new CreatedIdResult { Id = result.Id };
    }
}

// Story без возвращаемого значения
public class DeleteAccountStory : IStory<DeleteAccountStoryContext>
{
    private readonly ICrudService _crudService;

    public DeleteAccountStory(ICrudService crudService)
    {
        _crudService = crudService;
    }

    public async Task ExecuteAsync(DeleteAccountStoryContext context)
    {
        await _crudService.DeleteAsync<Account>(context.AccountId);
    }
}
```

#### 3. Регистрация в DI

Зарегистрируйте Story в DI-контейнере:

```csharp
// Регистрация вручную - вариант №1
services.AddScoped<IStory<CreateAccountStoryContext, CreatedIdResult>, CreateAccountStory>();
services.AddScoped<IStory<DeleteAccountStoryContext>, DeleteAccountStory>();

// Регистрация вручную - вариант №2
services.AddExecutors(options =>
{
    options.ExecutionTypes = new[] { typeof(IStory<>), typeof(IStory<,>) };
    options.ExecutorInterfaceType = typeof(IStoryExecutor);
    options.ExecutorImplementationType = typeof(StoryExecutor);
}, typeof(IStoryLibrary1), typeof(IStoryLibrary2));

// Регистрация вручную - вариант №3
services.AddExecutors(typeof(IStoryLibrary1), typeof(IStoryLibrary2));

// Или используйте автоматическую регистрацию
services.AddExecutorsFromAssembly(typeof(CreateAccountStory).Assembly);
```

#### 4. Использование через IStoryExecutor

Используйте `IStoryExecutor` для вызова Story:

```csharp
public class AccountController : ControllerBase
{
    private readonly IStoryExecutor _storyExecutor;

    public AccountController(IStoryExecutor storyExecutor)
    {
        _storyExecutor = storyExecutor;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AccountCreateDto dto)
    {
        var context = new CreateAccountStoryContext
        {
            Data = dto
        };

        var result = await _storyExecutor.Execute<CreatedIdResult>(context);
        
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var context = new DeleteAccountStoryContext
        {
            AccountId = id
        };

        await _storyExecutor.Execute(context);
        
        return NoContent();
    }
}
```

## Преимущества использования IStory

1. **Четкое разделение ответственности** - каждая Story выполняет одну конкретную задачу
2. **Простота тестирования** - изоляция бизнес-логики в отдельных классах
3. **Повторное использование** - возможность вызова Story из разных мест приложения
4. **Типобезопасность** - строгая типизация контекста и результата
5. **Масштабируемость** - легко добавлять новые Story без изменения существующего кода

### Лучшие практики

- Создавайте отдельный контекст для каждой Story
- Держите логику в Story простой и атомарной
- Используйте DI для передачи зависимостей
- Следуйте принципу единственной ответственности
- Предпочитайте асинхронное выполнение для всех операций (использование ExecuteAsync)

## CG4.DataAccess
### Интерфейс ICrudService

`ICrudService` - это основной интерфейс для работы с данными в CG4. Он предоставляет унифицированный API для выполнения CRUD-операций (Create, Read, Update, Delete) над сущностями в различных источниках данных.

### Основные возможности

- Асинхронные CRUD-операции
- Поддержка транзакций
- Гибкое построение запросов через выражения
- Преобразование типов сущностей
- Постраничный вывод данных

### Как использовать ICrudService?

#### 1. Получение данных

```csharp
// Получение по ID
var entity = await crudService.GetAsync<User>(123);

// Получение с преобразованием типа
var userDto = await crudService.GetAsync<User, UserDto>(123);

// Получение по условию
var user = await crudService.GetAsync<User>(opt => 
    opt.Where(x => x.Email == "user@example.com"));

// Получение списка с фильтрацией
var users = await crudService.GetAllAsync<User>(opt => 
    opt.Where(x => x.IsActive)
       .OrderBy(x => x.LastName));
```

#### 2. Создание, обновление, удаление

```csharp
// Создание новой записи
var newUser = new User { Name = "John", Email = "john@example.com" };
var created = await crudService.CreateAsync(newUser);

// Обновление существующей записи
user.Name = "John Updated";
var updated = await crudService.UpdateAsync(user);

// Удаление по сущности
await crudService.DeleteAsync(user);

// Удаление по ID
await crudService.DeleteAsync<User>(123);
```

### Построение запросов

`ICrudService` использует выражения для построения запросов через `IClassSqlOptions<T>`:

```csharp
// Получение пользователей по условию
var users = await crudService.GetAllAsync<User>(opt => opt
    .Where(x => x.Age > 18)
    .Where(x => x.IsActive)
    .OrderBy(x => x.LastName));

// Получение пользователей по условию и JOIN с таблицей 
var users = await _crudService.GetAsync<User, UserDto>(x => x
    .Where(x => x.Age > 18)
    .JoinLeft<Country, long>(r => r.CountryId, "Country"));

// Получение матчей по условию и несколькими JOIN с таблицей 
var match = await _crudService.GetAsync<Match, MatchDto>(e => e
    .Where(x => x.IsActive)
    .JoinLeft<Team, long>(e => e.TeamOneId, "TeamOne")
    .JoinLeft<Team, long?>(e => e.TeamTwoId, "TeamTwo")
    .JoinLeft<Tournament, long?>(e => e.TournamentId, "Tournament")
    .JoinLeft<TournamentStage, long?>(e => e.TournamentStageId, "TournamentStage"));
```

### Исполнение SQL запросов

`ISqlRepositoryAsync` предоставляет низкоуровневый доступ к базе данных через SQL-запросы. Этот интерфейс является частью `ICrudService` и позволяет выполнять прямые SQL-запросы, когда высокоуровневого API недостаточно.

#### Основные методы

1. **QueryAsync<T>** - получение одной записи по SQL-запросу
2. **QueryListAsync<T>** - получение списка записей по SQL-запросу
3. **ExecuteAsync** - выполнение SQL-команды (INSERT, UPDATE, DELETE)

#### Примеры использования

```csharp
// Получение одной записи
var user = await repository.QueryAsync<User>(
    "SELECT * FROM Users WHERE Email = @Email",
    new { Email = "user@example.com" }
);

// Получение списка записей
var users = await repository.QueryListAsync<User>(
    "SELECT * FROM Users WHERE Age > @Age ORDER BY LastName",
    new { Age = 18 }
);

// Выполнение команды
var affected = await repository.ExecuteAsync(
    "UPDATE Users SET IsActive = @IsActive WHERE Id = @Id",
    new { Id = 123, IsActive = true }
);
```

#### Поддерживаемые форматы параметров

1. Анонимный объект:
```csharp
var param = new { First = "A", Second = "B" };
```

2. Типизированный объект:
```csharp
var param = new UserFilter { MinAge = 18, IsActive = true };
```

3. Словарь:
```csharp
var param = new Dictionary<string, object>
{
    { "MinAge", 18 },
    { "IsActive", true }
};
```

4. Массив объектов (для batch-операций):
```csharp
var param = new User[] 
{ 
    new() { Name = "User1" },
    new() { Name = "User2" }
};
```

### Управление подключениями

CG4.DataAccess предоставляет `IConnectionFactory` для управления подключениями к источникам данных. Это позволяет абстрагировать создание подключений и централизовать их конфигурацию.

#### IConnectionFactory

Интерфейс предоставляет методы для создания подключений:

```csharp
// Асинхронное создание подключения
using var connection = await connectionFactory.CreateAsync();
// или с указанием строки подключения
using var connection = await connectionFactory.CreateAsync("connection_string");
```

#### Пример использования с транзакциями

```csharp
public class UserService
{
    private readonly ICrudService _crudService;
    private readonly IConnectionFactory _connectionFactory;

    public UserService(
        ICrudService crudService,
        IConnectionFactory connectionFactory)
    {
        _crudService = crudService;
        _connectionFactory = connectionFactory;
    }

    public async Task CreateUserWithProfile(UserDto userDto, ProfileDto profileDto)
    {
        using var connection = await _connectionFactory.CreateAsync();
        using var transaction = connection.BeginTransaction();

        try
        {
            var user = await _crudService.CreateAsync(new User 
            { 
                Name = userDto.Name 
            }, connection, transaction);

            var profile = await _crudService.CreateAsync(new Profile 
            { 
                UserId = user.Id,
                Bio = profileDto.Bio
            }, connection, transaction);

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}
```