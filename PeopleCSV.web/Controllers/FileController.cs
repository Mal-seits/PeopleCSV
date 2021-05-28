using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PeopleCsv.data;
using PeopleCSV.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleCSV.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly string _connectionString;
        private IConfiguration _configuration;

        public FileController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetFile")]
        public IActionResult GetFile(int amount)
        {
            var repo = new CSVRepository(_connectionString);
            var csv = repo.GetCSV(amount);
            var csvBytes = Encoding.UTF8.GetBytes(csv);
            return File(csvBytes, "APPLICATION/octet-stream", "People.csv");
        }
        [HttpPost]
        [Route("UploadFile")]
        public void UploadFile(UploadViewModel vm)
        {
            int commaIndex = vm.Base64String.IndexOf(',');
            string base64 = vm.Base64String.Substring(commaIndex + 1);
            var fileDate = Convert.FromBase64String(base64);
            var repo = new CSVRepository(_connectionString);
            repo.AddPeople(fileDate);
        }


    }
}
