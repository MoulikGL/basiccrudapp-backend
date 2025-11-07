using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BasicCrudApp.Data;
using BasicCrudApp.Models;

namespace BasicCrudApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _db;
    public UserController(AppDbContext db) => _db = db;

    [HttpPost("register")]
    public async Task<IActionResult> Register(User user)
    {
        var existing = await _db.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        if (existing != null) return BadRequest(new { message = "Email already registered." });

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return Ok(new { message = "User registered successfully." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(User login)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == login.Email && u.Password == login.Password);

        if (user == null) return Unauthorized(new { message = "Invalid email or password." });

        return Ok(new
        {
            message = "Login successful",
            user = new { user.FullName }
        });
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers() => await _db.Users.ToListAsync();
}