namespace AtriaNotificationApp.API.Models
{
   public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string Department { get; set; }
        public string Pno { get; set; }
        public string Role { get; set; }
    }
}