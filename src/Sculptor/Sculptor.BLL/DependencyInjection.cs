using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sculptor.BLL.Contracts;
using Sculptor.BLL.Implementations;

namespace Sculptor.BLL;

public static class DependencyInjection
{
    // Add services
    public static void AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<IAuthenticationService, AuthenticationService>()
            .AddScoped<ITokenService, TokenService>();
    }
}
