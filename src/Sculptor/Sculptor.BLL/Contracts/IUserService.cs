using Sculptor.Common.Models.User;

namespace Sculptor.BLL.Contracts;

public interface IUserService
{
    // Retrieve a user from the DB by id asyncronously
    Task<UserVM> GetUserByIdAsync(string id);

    // TODO: Add in controller
    // Retrieve a user from the DB by username asyncronously
    Task<UserVM> GetUserByUsernameAsync(string username);

    // Update the current users information in the DB asyncronously
    Task<UserVM> UpdateUserAsync(string username, UserUM userUM);

    // Delete a user by id asyncronously
    Task DeleteUserAsync(string username);

    // TODO: Add in controller
    // Get all users from the DB asynchronously
    Task<List<UserVM>> GetAllUsersAsync();
}
