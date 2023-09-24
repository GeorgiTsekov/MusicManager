using Microsoft.AspNetCore.Identity;
using MusicManager.API.Common.Models;
using MusicManager.API.Utils;

namespace MusicManager.API.Data.Models
{
    public class User : IdentityUser, IEntity, IDeletableEntity
    {
        public User()
        {
            Bands = new HashSet<Band>();
            Money = MMConstants.DEFAULT_MONEY;
        }

        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int Money { get; set; }
        public virtual ICollection<Band> Bands { get; set; }
    }
}
