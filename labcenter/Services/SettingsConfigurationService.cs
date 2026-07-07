using labcenter.Models;
using labcenter.Properties;

namespace labcenter.Services
{
    public class SettingsConfigurationService : IConfigurationService
    {
        public LabCenterConfiguration Load()
        {
            return new LabCenterConfiguration(Settings.Default.PcId, Settings.Default.LaboratoryId);
        }

        public void Save(LabCenterConfiguration configuration)
        {
            Settings.Default.PcId = configuration.PcId;
            Settings.Default.LaboratoryId = configuration.LaboratoryId;
            Settings.Default.Save();
        }
    }
}
