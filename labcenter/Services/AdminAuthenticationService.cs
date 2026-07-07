namespace labcenter.Services
{
    public class AdminAuthenticationService : IAdminAuthenticationService
    {
        private const string AdminUser = "admin23";
        private const string AdminPassword = "23admin@";

        public bool Validate(string user, string password)
        {
            return user == AdminUser && password == AdminPassword;
        }
    }
}
