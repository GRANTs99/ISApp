using System.ComponentModel.DataAnnotations.Schema;

namespace ISApi.Model
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string UserName { get; set; } 
        public string Name { get; set; }
        public int Age { get; set; }
        public string About { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
