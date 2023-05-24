using CDR_API.Contexts;
using CDR_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using CDR_API.Models;
using CDR_API.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        [Route("/readdata")]
        [HttpGet]
        public IActionResult ReadData()
        {
            using (var streamReader = new StreamReader(@"C:\Users\feren\Desktop\DWO\techtest_cdr.csv")) 
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture)) 
                {
                    var records = csvReader.GetRecords<CDR>();
                    
                    foreach (var record in records) 
                    {
                        var cdr = new CDR_Table()
                        {
                            caller_id = record.caller_id,
                            recipient = record.recipient,
                            call_date = record.call_date,
                            end_time = record.end_time,
                            duration = record.duration,
                            cost = record.cost,
                            reference = record.reference,
                            currency = record.currency
                        };

                        _cdrRepo.AddRecord(cdr);
                    }
                }
            }
            return Ok("Success!!");
        }
        
        [Route("/allrecords")]
        [HttpGet]
        public IActionResult GetAllRecords()
        {
            var cdrList = _cdrRepo.GetAllRecords();

            return Ok(cdrList);
        }


        // average call cost, 
        [Route("/averagecost")]
        [HttpGet]
        public IActionResult GetAverageCost()
        {
            var cdrList = _cdrRepo.GetAllRecords();

            var averageCost = cdrList.Average(x => x.cost);

            return Ok(averageCost);
        }
        
        // longest calls, 
        [Route("/longestcalls")]
        [HttpGet]
        public IActionResult GetLongestCalls()
        {
            var cdrList = _cdrRepo.GetAllRecords();

            var longestCalls = cdrList.OrderByDescending(item => item.duration)
                                      .Take(10)
                                      .ToList();


            return Ok(longestCalls);
        }

        // average number of calls per day
        [Route("/avgnrofcalls")]
        [HttpGet]
        public IActionResult AverageNumberOfCallsPerDay()
        {
            var cdrList = _cdrRepo.GetAllRecords();

            var dateList = new List<DateTime>();

            foreach (var item in cdrList)
            {
                if (!dateList.Contains(DateTime.Parse(item.call_date)))
                {
                    dateList.Add(DateTime.Parse(item.call_date));
                }
            }

            var orderedList = dateList.OrderByDescending(item => item).ToList();

            var pairs = new List<KeyValuePair<DateTime, int>>();

            foreach (var date in orderedList)
            {
                var pair = new KeyValuePair<DateTime, int>(date, 0);
                foreach (var item in cdrList)
                {
                    if (date == DateTime.Parse(item.call_date))
                    {
                        pair = new KeyValuePair<DateTime, int>(date, pair.Value + 1);
                    }
                }
                pairs.Add(pair);
            }

            foreach (var pair in pairs)
            {
                Console.WriteLine(pair);
            }

            var AverageNumberOfCallsPerDay = (int)pairs.Average(item => item.Value);

            return Ok(AverageNumberOfCallsPerDay);
        }
    }
}
