using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Palmfit.Data.Entities;
using Palmfit.Data.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text;
using API.Extensions;
using Palmfit.Core.Services;
using Palmfit.Core.Implementations;
using Core.Services;
using Core.Implementations;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
    .AddEnvironmentVariables()
    .Build();

var maxUserWatches = configuration.GetValue<int>("MaxUserWatches");
if (maxUserWatches > 0)
{
    var fileSystemWatcher = new FileSystemWatcher("/")
    {
        EnableRaisingEvents = true
    };
    var maxFiles = maxUserWatches / 2;
    fileSystemWatcher.InternalBufferSize = maxFiles * 4096;
}

//Repo registration
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IFoodInterfaceRepository, FoodInterfaceRepository>();
builder.Services.AddScoped<ITransactionsRepository, TransactionsRepository>();
builder.Services.AddScoped<IWalletRepository, WalletRepository>();
builder.Services.AddScoped<IUserInviteRepository, UserInviteRepository>();


// Configure JWT authentication options-----------------------
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
//jwt configuration ends-------------



// Add services to the container.
builder.Services.AddControllers();

//Calling the extention method
builder.Services.AddDbContextAndConfiguration(builder.Configuration);

// Database connection and Identity configuration
//builder.Services.AddDbContext<PalmfitDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Identity role registration with Stores and default token provider
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<PalmfitDbContext>()
    .AddDefaultTokenProviders();


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