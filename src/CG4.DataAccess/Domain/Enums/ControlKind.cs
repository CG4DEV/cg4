namespace CG4.DataAccess.Domain.Enums
{
    /// <summary>
    /// Вид контроля.
    /// </summary>
    public enum ControlKind
    {
        /// <summary>
        /// Неизвестный вид контроля.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Постоянный с ежемесячной информацией.
        /// </summary>
        Month = 1,

        /// <summary>
        /// Постоянный с ежеквартальной информацией.
        /// </summary>
        Quarter = 2,

        /// <summary>
        /// Постоянный с информацией по полугодиям.
        /// </summary>
        HalfYear = 3,

        /// <summary>
        /// Постоянный с информацией по годам.
        /// </summary>
        Year = 4,

        /// <summary>
        /// Однократный
        /// </summary>
        One = 5,
    }
}
