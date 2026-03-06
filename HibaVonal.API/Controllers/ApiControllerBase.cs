using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HibaVonal.API.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {
        protected int CurrentUserId
        {
            get
            {
                var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(claim))
                    throw new UnauthorizedAccessException("Unauthorized access");

                return int.Parse(claim);
            }
        }
    }
}
