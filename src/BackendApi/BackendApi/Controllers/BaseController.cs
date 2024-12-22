using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    
        public abstract class BaseController : ControllerBase
        {
         
            public User user => (User)HttpContext.Items["User"];
        }
    
}
