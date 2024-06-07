using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sculptor.BLL.Contracts;
using Sculptor.Common.Models.User;
using Sculptor.Common.Utilities;

namespace Sculptor.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
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

        // Update user by username asynchronously

        // TODO: Make a method to check the user by username in IAuthenticationService

        [HttpPatch("{username}")]
        public async Task<ActionResult<UserVM>> EditUser([FromBody] UserUM userUM, string username)
        {
            var user = userService.GetUserByUsernameAsync(username);
            if (!await this.authenticationService.CheckIfUserExistsAsync(username))
                return NotFound();

            return await userService.UpdateUserAsync(user.Id, userUM);
        }

        // TODO: Implement DeleteUserByUsername

        // Delete user by username asynchronously
        //[HttpDelete("{username}")]
        //public async Task DeleteUser(string username)
        //{
        //    await this.userService.DeleteUserAsync(username);
        //}
    }
}
