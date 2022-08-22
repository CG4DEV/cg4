using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CG4.DataAccess.Domain
{
    public interface IEntityBase : IEntity
    {

        DateTimeOffset CreateDate { get; set; }

        DateTimeOffset UpdateDate { get; set; }
    }

    /// <summary>
    ///     Базовая сущность.
    /// </summary>
    public abstract class EntityBase : IEntityBase
    {
        /// <summary>
        ///     Идентификатор сущности.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        ///     Дата создания сущности.
        /// </summary>
        [Column("create_date")]
        [Editable(false)]
        public DateTimeOffset CreateDate { get; set; }

        /// <summary>
        ///     Дата обновления сущности.
        /// </summary>
        [Column("update_date")]
        public DateTimeOffset UpdateDate { get; set; }
    }
}