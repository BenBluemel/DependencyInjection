namespace WebApplication.Models
{
    public class User : IUser
    {
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Username { get; set; }
    }
}