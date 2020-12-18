namespace MvvmLightLogin.Models
{
    //User Model
    public class User
    {
        public UserCredential UserCredential { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string ServiceStateDate { get; set; }
    }

    public class UserCredential
    {
        public UserCredential(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
