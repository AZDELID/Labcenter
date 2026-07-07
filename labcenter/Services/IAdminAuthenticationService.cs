namespace labcenter.Services
{
    public interface IAdminAuthenticationService
    {
        bool Validate(string user, string password);
    }
}
