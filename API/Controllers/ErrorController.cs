using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Override the BaseControllers Route
    // {code} is the http status code
    [Route("errors/{code}")]
    // Tell swagger to ignore this
    [ApiExplorerSettings(IgnoreApi = true )]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }    
    }
}