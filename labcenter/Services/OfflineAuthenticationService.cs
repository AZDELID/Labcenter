using System.Threading.Tasks;
using labcenter.Models;

namespace labcenter.Services
{
    public class OfflineAuthenticationService : IUserAuthenticationService
    {
        public Task<AuthenticatedUser> LoginAsync(string codigo, string password)
        {
            if (codigo == "test" && password == "1234")
                return Task.FromResult(new AuthenticatedUser(999, "Usuario Prueba offline"));

            if (codigo == "admin" && password == "admin")
                return Task.FromResult(new AuthenticatedUser(1, "Administrador"));

            return Task.FromResult<AuthenticatedUser>(null);
        }
    }
}
