namespace CG4.Story
{
    /// <summary>
    /// Интерфейс описывающий возращаемое значение из стори.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResult<T> : IResult
    {
    }

    /// <summary>
    /// Интерфейс описывающий конекст стори без возвращаемого значения.
    /// </summary>
    public interface IResult
    {
    }
}
