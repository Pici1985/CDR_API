using CDR_API.Entities;
using CDR_API.Models;
using System.Runtime.ConstrainedExecution;

namespace CDR_API.Repositories.Interfaces
{
    public interface ICDR_Repository
    {
        IEnumerable<CDR> GetAllRecords();
        void AddRecord(CDR_Table record);
    }
}
