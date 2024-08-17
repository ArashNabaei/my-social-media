using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace my_social_media.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected int GetUserIdFromClaims()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return int.Parse(userId);
        }
    }
}
