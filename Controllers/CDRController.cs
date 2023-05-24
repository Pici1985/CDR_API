using CDR_API.Contexts;
using CDR_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CDR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CDRController : ControllerBase
    {
        internal readonly ICDR_Repository _cdrRepo;

        public CDRController(ICDR_Repository cdrRepo)
        {
            _cdrRepo = cdrRepo;
        }

        [Route("/allrecords")]
        [HttpGet]
        public IActionResult Get()
        {
            var cdrList = _cdrRepo.GetAllRecords();

            return Ok(cdrList);
        }
    }
}
