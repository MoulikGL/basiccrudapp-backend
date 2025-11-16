using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BasicCrudApp.Data;
using BasicCrudApp.Models;
using BasicCrudApp.Services;

namespace BasicCrudApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly JwtService _jwt;
    public UserController(AppDbContext db, JwtService jwt)
    {
        _db = db;
        _jwt = jwt;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(User user)
    {
        var existing = await _db.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        if (existing != null) return BadRequest(new { message = "Email already registered." });

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return Ok(new { message = "User registered successfully!" });
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(User login)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == login.Email && u.Password == login.Password);

        if (user == null) return Unauthorized(new { message = "Invalid email or password." });

        var token = _jwt.GenerateToken(user.Email, user.FullName, user.IsAdmin);

        return Ok(new
        {
            message = "User logged in successfully!",
            token,
            user = new
            {
                user.Id,
                user.FullName,
                user.IsAdmin
            }
        });
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers() => await _db.Users.ToListAsync();

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, User input)
    {
        if (id != input.Id) return BadRequest();
        var user = await _db.Users.FindAsync(id);
        if (user is null) return NotFound();

        user.FullName = input.FullName;
        user.PhoneNumber = input.PhoneNumber;
        user.Address = input.Address;
        user.Company = input.Company;
        user.Email = input.Email;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        if (id == 1) return BadRequest(new { message = "Cannot delete the Administrator." });

        var user = await _db.Users.FindAsync(id);
        if (user is null) return NotFound();

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}