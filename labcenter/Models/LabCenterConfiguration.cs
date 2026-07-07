namespace labcenter.Models
{
    public class LabCenterConfiguration
    {
        public LabCenterConfiguration(int pcId, int laboratoryId)
        {
            PcId = pcId;
            LaboratoryId = laboratoryId;
        }

        public int PcId { get; private set; }
        public int LaboratoryId { get; private set; }
    }
}
