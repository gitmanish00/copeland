using DeviceDataProcessing.Business;
using DeviceDataProcessing.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace DeviceDataProcessing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeviceDataController : ControllerBase
    {
        [HttpPost(Name = "MergeFiles")]
        public void MergeFiles()
        {
            var fileOperation = new FileOperation();
            var process = new ProcessFiles(fileOperation);
            process.Process(".\\DataAccess\\JSON\\DeviceDataFoo1.json", ".\\DataAccess\\JSON\\DeviceDataFoo2.json", ".\\DataAccess\\JSON\\output.json");
        }
        
    }
}
