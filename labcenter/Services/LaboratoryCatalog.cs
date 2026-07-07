using System.Collections.Generic;

namespace labcenter.Services
{
    public class LaboratoryCatalog : ILaboratoryCatalog
    {
        private const int CurrentLaboratoryCount = 4;

        public IEnumerable<int> GetAvailableLaboratories()
        {
            for (int laboratory = 1; laboratory <= CurrentLaboratoryCount; laboratory++)
                yield return laboratory;
        }
    }
}
