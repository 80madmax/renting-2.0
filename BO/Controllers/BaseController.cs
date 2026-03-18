using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BO.Controllers
{
    public class BaseController : Controller
    {
        protected string LoggedUserId => User.FindFirstValue(ClaimTypes.NameIdentifier);        
        protected int LoggedUserIdAsInt => int.TryParse(LoggedUserId, out var id) ? id : 0;
    }
}
