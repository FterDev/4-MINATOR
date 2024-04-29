using FourMinator.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;





var builder = WebApplication.CreateBuilder(args);


var connectionString = "server=localhost;port=3306;database=fourminator;user=fmadm;password=4minatorDev24";
var serverVersion = MySqlServerVersion.AutoDetect(connectionString);


builder.Services.AddDbContext<AuthContext>(options => options.UseMySql(connectionString, serverVersion));
builder.Services.AddControllers();
builder.Services.AddScoped<IIdentityProviderAuthenticator, IdentityProviderAuthenticator>( x => new IdentityProviderAuthenticator(x.GetRequiredService<AuthContext>()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (args.FirstOrDefault() is { } arg && arg.StartsWith("newIdentityProvider="))
{

    var dbContext = app.Services.GetRequiredService<AuthContext>();
    var identityProviderName = arg.Split('=')[1];
    var newAuthenticator = new IdentityProviderAuthenticator(dbContext);
    newAuthenticator.CreateIdentityProvider(identityProviderName);
    var authKey = newAuthenticator.GenerateAuthKey();
    newAuthenticator.SaveIdentityProvider();
    
    Console.WriteLine($"Identity Provider {identityProviderName} created with AuthKey: {authKey}");
    return;
}


app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();



app.Run();
