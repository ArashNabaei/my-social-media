using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace my_social_media.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected int GetUserIdFromClaims()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userIdClaim, out int userId))
            {
                return userId;
            }

            throw new UnauthorizedAccessException("User ID not found in claims.");
        }

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
