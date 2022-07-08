using System.ComponentModel.DataAnnotations.Schema;

namespace ISApi.Model
{
    public class Photo
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
