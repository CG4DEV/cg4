namespace CG4.DataAccess
{
    /// <summary>
    /// Настройки соединения.
    /// </summary>
    public interface IConnectionSettings
    {
        /// <summary>
        /// Строка подключения.
        /// </summary>
        public string ConnectionString { get; set; }
    }
}