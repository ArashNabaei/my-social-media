using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace my_social_media.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private int? _userId;

        protected int UserId
        {
            get
            {
                if (!_userId.HasValue)
                {
                    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (int.TryParse(userIdClaim, out var userId))
                    {
                        _userId = userId;
                    }
                    else
                    {
                        throw new Exception("User ID not found in claims.");
                    }
                }
                return _userId.Value;
            }
        }
    }
}
