using Microsoft.AspNetCore.Mvc;

namespace my_social_media.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected void SetAuthorizationHeader()
        {
            if (Request.Headers.ContainsKey("Authorization"))
            {
                var token = Request.Headers["Authorization"].ToString();
                HttpContext.Request.Headers["Authorization"] = token;
            }
        }
    }
}
