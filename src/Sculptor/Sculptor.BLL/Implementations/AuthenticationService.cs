using Microsoft.AspNetCore.Identity;
using Sculptor.BLL.Contracts;
using Sculptor.Common.Models.User;
using Sculptor.DAL.Data;
using Sculptor.DAL.Models;

namespace Sculptor.BLL.Implementations;

internal class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly SculptorDbContext dbContext;
    private readonly RoleManager<IdentityRole> roleManager;

    // Add dependency injections
    public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager, SculptorDbContext dbContext, RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.dbContext = dbContext;
        this.roleManager = roleManager;
    }

    // Check if the password a user has entered to log in is correct asyncronously
    public async Task<bool> CheckIfPasswordIsCorrectAsync(string username, string password)
    {
        // Find the user in the DB
        var user = await this.userManager.FindByNameAsync(username);

        if (user is null)
            return false;

        // Check if password is correct
        return await this.userManager.CheckPasswordAsync(user, password);
    }

    // Check if a user exists in the DB using username asyncronously
    public async Task<bool> CheckIfUserExistsAsync(string username)
    {
        return await this.userManager.FindByNameAsync(username) is not null;
    }

    // Check if a user exists in the DB using id asyncronously
    public async Task<bool> CheckIfUserExistsByIdAsync(string id)
    {
        return await this.userManager.FindByIdAsync(id) is not null;
    }

    // Add a user to the DB asyncronously
    public async Task<IdentityResult> CreateUserAsync(UserIM userIM)
    {
        // Create a new user object
        var user = new User()
        {
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = userIM.Username,
            FirstName = userIM.FirstName,
            LastName = userIM.LastName,
        };

        // Attempt to create user
        var result = await userManager.CreateAsync(user, userIM.Password);

        // If user is not created successfully
        if (!result.Succeeded)
            return result;

        // Create retailer role
        if (!await this.roleManager.RoleExistsAsync(UserRoles.Retailer))
        {
            await this.roleManager.CreateAsync(new IdentityRole(UserRoles.Retailer));
        }

        // Create deliverer role
        if (!await this.roleManager.RoleExistsAsync(UserRoles.Deliverer))
        {
            await this.roleManager.CreateAsync(new IdentityRole(UserRoles.Retailer));
        }

        // Create admin role
        if (!await this.roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await this.roleManager.CreateAsync(new IdentityRole(UserRoles.Retailer));
        }

        // Add role to the user
        if (await this.roleManager.RoleExistsAsync(userIM.Role))
        {
            await this.userManager.AddToRoleAsync(user, userIM.Role);
        }

        return result;
    }
}
