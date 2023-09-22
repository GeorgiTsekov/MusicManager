using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MusicManager.API.Utils;

namespace MusicManager.API.Models.Domain
{
    public class Manager : IdentityUser
    {
        public Manager()
        {
            this.Bands = new HashSet<Band>();
            this.Money = MMConstants.DEFAULT_MONEY;
        }

        public ICollection<Band> Bands { get; set; }
        public int Money { get; set; }
    }
}
