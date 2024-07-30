namespace CaesarsTest.API.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }

        public User(int userId, string userName, string firstName, string lastName, string role)
        {
            UserId = userId;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }
    }
}
