namespace ITL.DataAccess
{
    /// <summary>
    /// Настройки соеденения.
    /// </summary>
    public interface IConnectionSettings
    {
        /// <summary>
        /// Строка подключения.
        /// </summary>
        public string ConnectionString { get; set; }
    }
}