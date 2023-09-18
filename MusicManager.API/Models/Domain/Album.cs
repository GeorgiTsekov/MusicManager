using MusicManager.API.Common.Models;

namespace MusicManager.API.Models.Domain
{
    public class Album : BaseDeletableModel<int>
    {
        public Album()
        {
            this.Songs = new HashSet<Song>();
        }

        public string Name { get; set; }
        public string Descritpion { get; set; }
        public int BandId { get; set; }
        public virtual Band Band { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
