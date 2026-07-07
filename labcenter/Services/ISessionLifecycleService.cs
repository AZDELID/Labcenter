using System.Threading.Tasks;

namespace labcenter.Services
{
    public interface ISessionLifecycleService
    {
        Task StartSessionAsync(int userId, int pcId);
        Task EndSessionAsync(int pcId);
    }
}
