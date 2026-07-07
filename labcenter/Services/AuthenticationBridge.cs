using System.Threading.Tasks;
using labcenter.Models;

namespace labcenter.Services
{
    public class AuthenticationBridge
    {
        private readonly IUserAuthenticationService authenticationService;
        private readonly ISessionLifecycleService sessionLifecycleService;

        public AuthenticationBridge(
            IUserAuthenticationService authenticationService,
            ISessionLifecycleService sessionLifecycleService)
        {
            this.authenticationService = authenticationService;
            this.sessionLifecycleService = sessionLifecycleService;
        }

        public Task<AuthenticatedUser> LoginAsync(string codigo, string password)
        {
            return authenticationService.LoginAsync(codigo, password);
        }

        public Task StartSessionAsync(int userId, int pcId)
        {
            return sessionLifecycleService.StartSessionAsync(userId, pcId);
        }

        public Task EndSessionAsync(int pcId)
        {
            return sessionLifecycleService.EndSessionAsync(pcId);
        }
    }
}
