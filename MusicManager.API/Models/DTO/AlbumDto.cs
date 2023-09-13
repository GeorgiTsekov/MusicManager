namespace MusicManager.API.Models.DTO
{
    public class AlbumDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descritpion { get; set; }
        public string BandName { get; set; }
        public int BandId { get; set; }
        public virtual BandDto Band { get; set; }
        public virtual ICollection<SongDto> Songs { get; set; }
    }
}
