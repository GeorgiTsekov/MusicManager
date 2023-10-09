using System.ComponentModel.DataAnnotations;

namespace MusicManager.Infrastructure.Models
{
    public abstract class BaseModel<TKey> : IEntity
    {
        [Key]
        public TKey Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
