using IdentityServerAuth;
using ISApi.Model;
using ISApi.Repository;
using ISApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
builder.Services.AddTransient<IRepository<AppUser>, SQLServerAppUserRepository>();
builder.Services.AddTransient<AppUserService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryClients(Config.Clients)
    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

var app = builder.Build();
app.UseIdentityServer();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
