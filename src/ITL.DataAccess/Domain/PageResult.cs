namespace ITL.DataAccess.Domain
{
    /// <summary>
    /// Page result.
    /// </summary>
    /// <typeparam name="TData">Data type</typeparam>
    public class PageResult<TData>
    {
        /// <summary>
        /// Data result.
        /// </summary>
        public IEnumerable<TData> Data { get; set; }

        /// <summary>
        /// Current page.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Общее количество
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// All filtered rows.
        /// </summary>
        public int FilteredCount { get; set; }
    }

    public class PageContext
    {
        /// <summary>
        /// Текущая страница.
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Количество выводимых строк.
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Быстрый поиск
        /// </summary>
        public string Search { get; set; }

        public PageContext()
        {
            Offset = 0;
            Limit = 250;
        }
    }
}