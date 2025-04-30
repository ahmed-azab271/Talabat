using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatAPIs.Errors;

namespace TalabatAPIs.Controllers
{
    //this controller to handel the Responce if the user want to access an Endpoint that not exicts 

    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public ActionResult Error (int code)
        {
            return NotFound(new ApiResponce(code));
        }
    }
}
