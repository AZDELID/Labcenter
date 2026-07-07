using System.Collections.Generic;

namespace labcenter.Services
{
    public interface ILaboratoryCatalog
    {
        IEnumerable<int> GetAvailableLaboratories();
    }
}
