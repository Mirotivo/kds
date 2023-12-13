using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;
    private readonly Dictionary<string, string> _users;
    private readonly IHostEnvironment _hostingEnvironment;

    private readonly kdsDbContext dbContext;
    private readonly IPasswordHasher<User> passwordHasher;
    private readonly IConfiguration _config;

    public UserController(ILogger<UserController> logger,
        Dictionary<string, string> users, IHostEnvironment hostingEnvironment, kdsDbContext dbContext, IPasswordHasher<User> passwordHasher, IConfiguration config)
    {
        _logger = logger;
        _users = users;
        _hostingEnvironment = hostingEnvironment;
        this.dbContext = dbContext;
        this.passwordHasher = passwordHasher;
        _config = config;
    }

    [HttpPost(Name = "register")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        // Check if user already exists
        if (await dbContext.Users.AnyAsync(u => u.Email == model.Email))
        {
            return BadRequest("A user with this email address already exists.");
        }

        // Create new user
        User user = new User
        {
            Email = model.Email
        };
        user.PasswordHash = passwordHasher.HashPassword(user, model.Password);

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        return Ok();
    }


    [HttpPost(Name = "login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

        if (user != null)
        {
            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

            if (result == PasswordVerificationResult.Success)
            {
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email)
            };

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(7),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]??"")),
                        SecurityAlgorithms.HmacSha256Signature)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
        }

        return Unauthorized();
    }


}
