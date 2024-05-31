using Sculptor.Common.Models.User;

namespace Sculptor.BLL.Contracts;

public interface IUserService
{
    // Retrieve a user from the DB by id asyncronously
    Task<UserVM> GetUserByIdAsync(string id);

    // Update the current users information in the DB asyncronously
    Task<UserVM> UpdateUserAsync(string id, UserUM userUM);

    // Delete a user by id asyncronously
    Task DeleteUserAsync(string id);
}
