using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // This attribute means less code. Add it to every controller
    [ApiController]  // Also maps parameters passed into the methods below

    // Add a route to get to the controller. All routes start with api
    [Route("api/[controller]")]
    
    public class BaseApiController : ControllerBase
    {
        
    }
}