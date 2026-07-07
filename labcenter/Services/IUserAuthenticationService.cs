using System.Threading.Tasks;
using labcenter.Models;

namespace labcenter.Services
{
    public interface IUserAuthenticationService
    {
        Task<AuthenticatedUser> LoginAsync(string codigo, string password);
    }
}
