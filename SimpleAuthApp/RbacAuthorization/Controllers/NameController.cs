using JwtAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using RbacAuthorization.Models;

namespace RbacAuthorization.Controllers
{
        [EnableCors("AllowAll")]
        [Route("api/[controller]")]
        [ApiController]
        public class NameController : ControllerBase
        {
        private readonly IJWTAuthenticationManager jWTAuthenticationManager;
        private readonly ITokenRefresher tokenRefresher;

        public NameController(IJWTAuthenticationManager jWTAuthenticationManager, ITokenRefresher tokenRefresher)
        {
            this.jWTAuthenticationManager = jWTAuthenticationManager;
            this.tokenRefresher = tokenRefresher;
        }

        // GET: api/Name
        [Authorize]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "New York", "New Jersey" };
        }

        // GET: api/Name/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "New Jersey";
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
            var token = jWTAuthenticationManager.Authenticate(userCred.Username, userCred.Password);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshCred refreshCred)
        {
            var token = tokenRefresher.Refresh(refreshCred);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }
    }
    
}