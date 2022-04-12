namespace CG4.Extensions
{
    /// <summary>
    /// Содержит расширения для работы с коллекциями
    /// </summary>
    public static class LinqExtension
    {
        /// <summary>
        /// Проверяет, что корллекция равна null или пуста
        /// </summary>
        /// <typeparam name="TSource">Тип коллекции</typeparam>
        /// <param name="source">Коллекция</param>
        /// <returns>True - коллекция пуста или равна null</returns>
        public static bool NullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source == null || !source.Any();
        }
    }
}
