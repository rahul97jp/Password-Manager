namespace PasswordManager.Models
{
    public class UpdatePasswordRequest
    {
        public string Category { get; set; }
        public string App { get; set; }
        public string UserName { get; set; }
        public string EncryptedPassword { get; set; }
    }
}
