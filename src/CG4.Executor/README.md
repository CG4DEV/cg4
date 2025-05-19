# CG4.Executor.Story - Руководство по использованию

## Интерфейс IStory

Интерфейс `IStory` представляет собой основной компонент для реализации бизнес-логики в архитектуре CG4. 
Он используется для создания инкапсулированных операций, которые работают с конкретным контекстом и могут возвращать или не возвращать результат.

### Основные принципы

- `IStory` реализует шаблон проектирования Command Pattern
- Выполняет одну атомарную бизнес-операцию
- Каждая Story должна иметь свой Context
- Поддерживает DI через конструктор

### Определение интерфейса

```csharp
// Интерфейс стори с возвращаемым значением
public interface IStory<in TStoryContext, TStoryResult> : IExecution<TStoryContext, TStoryResult>
    where TStoryContext : IResult<TStoryResult>
{
}

// Интерфейс стори без возвращаемого значения
public interface IStory<in TStoryContext> : IExecution<TStoryContext>
    where TStoryContext : IResult
{
}
```

## Как использовать IStory

### 1. Определение контекста

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

### 2. Реализация IStory

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

### 3. Регистрация в DI

Зарегистрируйте Story в DI-контейнере:

```csharp
// Регистрация вручную
services.AddScoped<IStory<CreateAccountStoryContext, CreatedIdResult>, CreateAccountStory>();
services.AddScoped<IStory<DeleteAccountStoryContext>, DeleteAccountStory>();

// Или используйте автоматическую регистрацию
services.AddExecutorsFromAssembly(typeof(CreateAccountStory).Assembly);
```

### 4. Использование через IStoryExecutor

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

## Лучшие практики

- Создавайте отдельный контекст для каждой Story
- Держите логику в Story простой и атомарной
- Используйте DI для передачи зависимостей
- Следуйте принципу единственной ответственности
- Предпочитайте асинхронное выполнение для всех операций (использование ExecuteAsync) 