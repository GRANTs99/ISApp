using FluentValidation;
using FluentValidation.AspNetCore;
using ISApi.Model;
using ISApi.Repository;
using ISApi.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ISApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssemblyContaining<RegistrationValidator>();
    options.RegisterValidatorsFromAssemblyContaining<UserProfileValidator>();
});
builder.Services.AddEndpointsApiExplorer();
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
builder.Services.AddTransient<IRepository<AppUser>, SQLServerAppUserRepository>();
builder.Services.AddTransient<IRepository<UserProfile>, SQLServerUserProfileRepository>();
builder.Services.AddTransient<IRepository<Photo>, SQLServerPhotoRepository>();
builder.Services.AddTransient<AppUserService>();
builder.Services.AddTransient<PhotoService>();
builder.Services.AddHttpClient();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:7183";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
