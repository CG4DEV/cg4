namespace CG4.Extensions
{
    /// <summary>
    /// Содержит расширения для проверки объектов на null.
    /// </summary>
    public static class ArgumentsExtension
    {
        /// <summary>
        /// Проверяет объект на null.
        /// </summary>
        /// <typeparam name="T">Тип объекта для проверки.</typeparam>
        /// <param name="arg">Проверяемый объект.</param>
        /// <param name="message">Сообщение об ошибке, если объект равен <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Если объект равен <see langword="null"/>.</exception>
        /// <returns>Возвращает переданный не пустой объект.</returns>
        public static T CheckNull<T>(this T arg, string message = null)
        {
            if (typeof(T) == typeof(string) && string.IsNullOrWhiteSpace(arg as string))
            {
                throw new ArgumentNullException(message);
            }

            if (arg == null)
            {
                throw new ArgumentNullException(message);
            }

            return arg;
        }

        /// <summary>
        /// Проверяет коллекцию.
        /// </summary>
        /// <param name="arg">Проверяемый объект.</param>
        /// <param name="message">Сообщение об ошибке, если объект равен <see langword="null"/>.</param>
        /// <typeparam name="T">Тип объекта для проверки.</typeparam>
        /// <returns>Список сущностей заданного типа T.</returns>
        /// <exception cref="ArgumentNullException">Коллекция равна <see langword="null"/> или пуста.</exception>
        public static IEnumerable<T> CheckNullOrEmpty<T>(this IEnumerable<T> arg, string message)
        {
            if (arg.NullOrEmpty())
            {
                throw new ArgumentNullException(message);
            }

            return arg;
        }
    }
}
