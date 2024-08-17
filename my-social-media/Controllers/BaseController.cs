using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace my_social_media.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected int UserId { get { return GetUserIdFromClaims(); } }

        protected int GetUserIdFromClaims()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userIdClaim, out int userId))
                return userId;

            throw new UnauthorizedAccessException("User ID not found in claims.");
        }

    }
}
