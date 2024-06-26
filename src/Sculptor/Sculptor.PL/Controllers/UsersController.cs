using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sculptor.BLL.Contracts;
using Sculptor.Common.Models.User;
using Sculptor.Common.Utilities;
using Sculptor.DAL.Models;

namespace Sculptor.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IAuthenticationService authenticationService;
        private readonly ICurrentUser currentUser;

        // Add dependency injections
        public UsersController (IUserService userService,  IAuthenticationService authenticationService, ICurrentUser currentUser)
        {
            this.userService = userService;
            this.authenticationService = authenticationService;
            this.currentUser = currentUser;
        }

        // Retrieve a user from the DB by username asyncronously
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserVM>> GetUserByUsernameAsync(string username)
        {
            if (!await this.authenticationService.CheckIfUserExistsAsync(username))
                return NotFound();

            return await this.userService.GetUserByUsernameAsync(username);
        }

        // Get all users from the DB asynchronously
        [HttpGet("All")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UserVM>>> GetAllUsersAsync()
        {
            return await this.userService.GetAllUsersAsync();
        }

        // Update user by username asynchronously
        [HttpPatch("{username}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserVM>> EditUser([FromBody] UserUM userUM, string username)
        {
            if (!await this.authenticationService.CheckIfUserExistsAsync(username))
                return NotFound();

            return await userService.UpdateUserAsync(username, userUM);
        }

        // Delete user by username asynchronously

        [HttpDelete("{username}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Response>> DeleteUser(string username)
        {
            await this.userService.DeleteUserAsync(username);

            return this.Ok(
                new Response
                {
                    Status = "User deleted successfully",
                    Message = "This user has been deleted successfully!"
                });
        }
    }
}
