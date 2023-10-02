using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PersonalFinancialManagement.API.Controllers.Base;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos;
using PersonalFinancialManagement.Models.Dtos.Users;
using PersonalFinancialManagement.Models.Entities.Identities;

namespace PersonalFinancialManagement.API.Controllers;

public class AuthController : ApiController
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly ApplicationDbContext _context;
    private readonly JwtOptions _jwtOptions;

    public AuthController(UserManager<User> userManager,
        RoleManager<Role> roleManager,
        ApplicationDbContext context, IOptions<JwtOptions> jwtOptions, ILogger<AuthController> logger) : base(logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
        _jwtOptions = jwtOptions.Value;
    }

    [HttpPost("register")]
    public async Task<IActionResult> PostUser(RegisterViewModel request)
    {
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Email = request.Email,
            UserName = request.UserName,
            PhoneNumber = request.PhoneNumber
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, request);
        return BadRequest(result);
    }

    // URL: GET: http://localhost:5001/api/users/{id}
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound($"Cannot found user with id: {id}");

        var userVm = new UserViewModel
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber
        };
        return Ok(userVm);
    }
    [HttpGet()]
    [Authorize]
    public async Task<IActionResult> GetByUserName()
    {
        var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
            return BadRequest();

        var userVm = new UserViewModel
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber
        };
        return Ok(userVm);
    }

    //[HttpPut("{id}")]
    //public async Task<IActionResult> PutUser(string id, [FromBody] UserCreateRequest request)
    //{
    //    var user = await _userManager.FindByIdAsync(id);
    //    if (user == null)
    //        return NotFound(new ApiNotFoundResponse($"Cannot found user with id: {id}"));

    //    user.FirstName = request.FirstName;
    //    user.LastName = request.LastName;
    //    user.Dob = DateTime.Parse(request.Dob);
    //    user.LastModifiedDate = DateTime.Now;

    //    var result = await _userManager.UpdateAsync(user);

    //    if (result.Succeeded)
    //    {
    //        return NoContent();
    //    }
    //    return BadRequest(new ApiBadRequestResponse(result));
    //}

    [HttpPut("{id}/change-password")]
    public async Task<IActionResult> PutUserPassword(string id, [FromBody] UserPasswordChangeRequest request)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound($"Cannot found user with id: {id}");

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

        if (result.Succeeded)
        {
            return NoContent();
        }
        return BadRequest(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                var checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);
                if (checkPassword) return Ok(GenerateToken(user));
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
            }
        }

        return BadRequest(ModelState);
    }

    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserName!)
        };
        var token = new JwtSecurityToken(_jwtOptions.ValidIssuer,
            _jwtOptions.ValidAudience,
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);


        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}