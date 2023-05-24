using CDR_API.Models;

namespace CDR_API.Repositories.Interfaces
{
    public interface ICDR_Repository
    {
        IEnumerable<CDR> GetAllRecords();
    }
}
