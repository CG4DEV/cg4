namespace ITL.Extensions
{
    /// <summary>
    /// Содержит расширения для работы с коллекциями.
    /// </summary>
    public static class LinqExtension
    {
        /// <summary>
        /// Проверяет, что коллекция равна <see langword="null"/> или пуста.
        /// </summary>
        /// <typeparam name="TSource">Тип коллекции.</typeparam>
        /// <param name="source">Коллекция.</param>
        /// <returns><see langword="true"/> - коллекция пуста или равна <see langword="null"/>.</returns>
        public static bool NullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source == null || !source.Any();
        }
    }
}
