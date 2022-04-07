namespace CG4.DataAccess.Domain.Enums
{
    /// <summary>
    /// Статусы контроля.
    /// </summary>
    public enum ControlStatus
    {
        /// <summary>
        /// Неизвестный статус контроля.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// На контроле.
        /// </summary>
        InControl = 1,

        /// <summary>
        /// Не требует контроля.
        /// </summary>
        NotRequired = 2,

        /// <summary>
        /// Снят с контроля.
        /// </summary>
        OutControl = 3,
    }
}
