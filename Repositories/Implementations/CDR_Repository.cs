using CDR_API.Contexts;
using CDR_API.Models;
using CDR_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CDR_API.Repositories.Implementations
{
    public class CDR_Repository : ICDR_Repository
    {
        internal readonly CDRDbContext _context;

        public CDR_Repository(CDRDbContext context)
        {
            _context = context;
        }
        public IEnumerable<CDR> GetAllRecords()
        {
            var cdrList = (from c in _context.CDR_Table
                           select new CDR() 
                           {
                                caller_id = c.caller_id,
                                recipient = c.recipient,
                                call_date = c.call_date,
                                end_time = c.end_time,
                                duration = c.duration,
                                cost = c.cost,
                                reference = c.reference,
                                currency = c.currency
                           }).ToList();

            return cdrList;
        }
    }
}
