using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sculptor.BLL.Contracts;
using Sculptor.Common.Models.User;
using Sculptor.DAL.Data;
using Sculptor.DAL.Models;

namespace Sculptor.BLL.Implementations;

internal class UserService : IUserService
{
    private readonly UserManager<User> userManager;
    private readonly SculptorDbContext dbContext;
    private readonly IMapper mapper;

    // Add dependency injections
    public UserService(UserManager<User> userManager, SculptorDbContext dbContext, IMapper mapper)
    {
        this.userManager = userManager;
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // Retrieve a user from the DB by id asyncronously
    public async Task<UserVM> GetUserByIdAsync(string id)
    {
        return this.mapper.Map<UserVM>(await userManager.Users
            .Where(usr => usr.Id == id)
            .FirstOrDefaultAsync());
    }

    // Retrieve a user from the DB by username asyncronously
    public async Task<UserVM> GetUserByUsernameAsync(string username)
    {
        return this.mapper.Map<UserVM>(await userManager.Users
            .Where(usr => usr.UserName == username)
            .FirstOrDefaultAsync());
    }

    // Update the current users information in the DB asyncronously
    public async Task<UserVM> UpdateUserAsync(string username, UserUM userUM)
    {
        // Retrieve the user data from the DB
        var user = await userManager.Users
            .Where(usr => usr.UserName == username)
            .FirstAsync();

        //Check if the username is changed and apply the changes
        if (userUM.Username != null)
            user.UserName = userUM.Username;

        //Check if the password is changed and apply the changes
        if (userUM.Password != null)
        {
            // Encrypt the new password
            var token = await this.userManager.GeneratePasswordResetTokenAsync(user);

            _ = await this.userManager.ResetPasswordAsync(user, token, userUM.Password);
        }

        //Check if the first name is changed and apply the changes
        if (userUM.FirstName != null)
            user.FirstName = userUM.FirstName;

        //Check if the last name is changed and apply the changes
        if (userUM.LastName != null)
            user.LastName = userUM.LastName;

        await this.userManager.UpdateAsync(user);

        return this.mapper.Map<UserVM>(user);
    }

    // Delete a user by username asyncronously
    public async Task DeleteUserAsync(string username)
    {
        // Retrieve the user
        var user = await userManager.Users
            .Where(usr => usr.UserName == username)
            .FirstAsync();

        if (user == null) return;

        await userManager.DeleteAsync(user);

        await dbContext.SaveChangesAsync();
    }

    // Get all users from the DB asynchronously
    public async Task<List<UserVM>> GetAllUsersAsync()
    {
        return await this.userManager.Users
            .Where(u => u.UserName != null)
            .ProjectTo<UserVM>(this.mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
