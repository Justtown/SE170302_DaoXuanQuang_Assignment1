namespace SE170302_DaoXuanQuang_Assignment01_FrontEnd.DTOs
{
    public class LoginDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class UserSessionDto
    {
        public short AccountId { get; set; }
        public string AccountName { get; set; } = null!;
        public string AccountEmail { get; set; } = null!;
        public int Role { get; set; }
    }
}