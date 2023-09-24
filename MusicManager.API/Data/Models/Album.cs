using MusicManager.API.Common.Models;

namespace MusicManager.API.Data.Models
{
    public class Album : BaseDeletableModel<int>
    {
        public Album()
        {
            Songs = new HashSet<Song>();
        }

        public string Name { get; set; }
        public string Descritpion { get; set; }
        public int BandId { get; set; }
        public virtual Band Band { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
