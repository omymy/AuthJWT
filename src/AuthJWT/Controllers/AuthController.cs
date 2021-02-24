using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AuthJWT.Controllers
{
    [Route("/api/auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IJWTService _jwtService;
        private AppDbContext _db;
        public AuthController(IJWTService jwtService, AppDbContext db)
        {
            _jwtService = jwtService;
            _db = db;
        }

        [HttpPost("token")]
        public async Task<IActionResult> GenerateToken(AuthRequest model)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName);
            if (user == null)
                return Unauthorized();
            if (user.Password != Encrypt.MDString(model.Password))
                return Unauthorized();

            var claims = new List<Claim>()
            {
                new Claim("userid", user.Id.ToString()),
                new Claim("username", user.UserName),
                new Claim("can_view", "true")
            };

            if (user.IsAdmin)
            {
                claims.Add(new Claim("can_delete", "true"));
            }

            return Ok(new
            {
                token = _jwtService.GenerateToken(claims)
            });
        }
    }
}
