using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
//using Sculptor.BLL;
using Sculptor.DAL.Data;
using Sculptor.DAL.Models;
//using Sculptor.PL.Helpers;
//using Sculptor.PL.Mapping;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Entity Framework
builder.Services.AddDbContext<SculptorDbContext>(options =>
   options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")!, o =>
   {
       o.MigrationsAssembly(typeof(Program).Assembly.FullName);
       o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
   }));

// User Identity
builder.Services
    .AddIdentity<User, IdentityRole>(options =>
    {
        /*options.SignIn.RequireConfirmedEmail = true;*/
    })
    .AddEntityFrameworkStores<SculptorDbContext>()
    .AddDefaultTokenProviders();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();