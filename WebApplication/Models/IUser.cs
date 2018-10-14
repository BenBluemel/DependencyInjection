namespace WebApplication.Models
{
    public interface IUser
    {
        string FamilyName { get; set; }
        string GivenName { get; set; }
        int Id { get; set; }
        string Username { get; set; }
    }
}