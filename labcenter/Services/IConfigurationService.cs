using labcenter.Models;

namespace labcenter.Services
{
    public interface IConfigurationService
    {
        LabCenterConfiguration Load();
        void Save(LabCenterConfiguration configuration);
    }
}
