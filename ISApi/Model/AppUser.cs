namespace ISApi.Model
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public UserProfile? Profile { get; set; }
        public List<Photo>? Photo { get; set; }
}
}
