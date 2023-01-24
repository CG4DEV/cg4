namespace CG4.Executor
{
    /// <summary>
    /// Интерфейс, описывающий возращаемое значение из стори.
    /// </summary>
    /// <typeparam name="T">Контекст стори.</typeparam>
    public interface IResult<T> : IResult
    {
    }

    /// <summary>
    /// Интерфейс, описывающий контекст стори без возвращаемого значения.
    /// </summary>
    public interface IResult
    {
    }
}
