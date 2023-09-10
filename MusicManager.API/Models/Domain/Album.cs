namespace MusicManager.API.Models.Domain
{
    public class Album
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Descritpion { get; set; }

        public Guid BandId { get; set; }
        public virtual Band Band { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
