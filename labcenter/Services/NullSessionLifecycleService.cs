using System.Threading.Tasks;

namespace labcenter.Services
{
    public class NullSessionLifecycleService : ISessionLifecycleService
    {
        public Task StartSessionAsync(int userId, int pcId)
        {
            return Task.CompletedTask;
        }

        public Task EndSessionAsync(int pcId)
        {
            return Task.CompletedTask;
        }
    }
}
