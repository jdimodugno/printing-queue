using API.Data;
using API.Domain;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using QueueSDK.Domain;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrintController : Controller
    {
        [HttpGet]
        public IActionResult GetPrintings()
        {
            var res = PrintingRepository.ReadPrintings();
            return Json(res);
        }

        [HttpGet("printed/{name}")]
        public IActionResult GetPrintings([FromRoute] string name)
        {
            PrintedDocument doc = PrintingRepository.CheckPrintRequestStatus(name);
            if (doc != null) return Json(doc);
            return StatusCode(404, "Not Found");
        }

        [HttpPost]
        public IActionResult Print([FromBody] Payload doc)
        {
            if (doc.Path.Contains(";")) return StatusCode(500, "Invalid Document Path");
            System.Console.WriteLine($"{doc.Path} entered with priority {doc.Priority}");
            PrintingJobsService.GetInstance().SendToQueue(doc);
            return Ok();
        }
    }
}
