using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Sculptor.BLL.Contracts;
using Sculptor.Common.Models.Login;
using Sculptor.Common.Models.User;
using Sculptor.Common.Utilities;

namespace Sculptor.PL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService authService;
    private readonly ITokenService tokenService;

    // Add dependency injections
    public AuthController(IAuthenticationService authService, ITokenService tokenService)
    {
        this.authService = authService;
        this.tokenService = tokenService;
    }

    // Create a user register asyncronously
    [HttpPost("Register")]
    public async Task<ActionResult<Response>> RegisterUserAsync([FromBody] UserIM userIM)
    {
        // Check if the user already exists
        if (await this.authService.CheckIfUserExistsAsync(userIM.Username))
            return this.BadRequest(new Response
            {
                Status = "User creation failure",
                Message = "This user already exists!"
            });

        // Create the user
        var response = await this.authService.CreateUserAsync(userIM);

        // If the user fails creating
        if (!response.Succeeded)
            return this.BadRequest(new Response
            {
                Status = "User creation failure",
                Message = response.Errors.First().Description,
            });

        // Confirm user creation
        return this.Ok(
            new Response
            {
                Status = "User created successfully",
                Message = "This user has been created successfully!"
            });
    }

    // Create a user login asyncronously
    [HttpPost("Login")]
    public async Task<ActionResult<LoginVM>> LoginUserAsync([FromBody] LoginIM loginIM)
    {
        // Check if the user exists
        if (!await this.authService.CheckIfUserExistsAsync(loginIM.Username))
            return this.NotFound(new Response
            {
                Status = "User not found",
                Message = "This user does not exist!"
            });

        // Check if the password is correct
        if (!await this.authService.CheckIfPasswordIsCorrectAsync(loginIM.Username, loginIM.Password))
            return this.BadRequest(new Response
            {
                Status = "Incorrect password",
                Message = "The password is incorrect!"
            });

        // Create a token (valid for 30 min)
        var token = await this.tokenService.CreateTokenForUserAsync(loginIM.Username);

        // Confirm the user has been logged in
        return this.Ok(
            new LoginVM
            {
                Status = "User logged in successfully",
                Message = "This user has been logged in successfully!",
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            });
    }
}