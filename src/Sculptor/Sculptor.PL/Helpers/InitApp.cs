using Sculptor.BLL.Contracts;
using Sculptor.Common.Models.User;

namespace Sculptor.PL.Helpers;

public static class InitApp
{
    // Initialize the app and create users if there are none in the DB
    public static async Task InitAppAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var authenticationService = scope.ServiceProvider.GetRequiredService<IAuthenticationService>();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

        // Check if there are users in the DB
        if ((await userService.GetAllUsersAsync()).Count() != 0)
            return;

        // Create users
        var users = new List<UserIM>
        {
            new UserIM
            {
                Username = "admin",
                Password = "AdminP@55",
                FirstName = "Dimitar",
                LastName = "Yonchev",
                Role = "Admin"
            },
            new UserIM
            {
                Username = "deliverer",
                Password = "DelivererP@55",
                FirstName = "Yordan",
                LastName = "Kolev",
                Role = "Deliverer"
            },
            new UserIM
            {
                Username = "retailer",
                Password = "RetailerP@55",
                FirstName = "Ivana",
                LastName = "Novkova",
                Role = "Retailer"
            }
        };

        // Add the users to the DB
        foreach(var user in users)
        {
            await authenticationService.CreateUserAsync(user);
        }
    }
}
