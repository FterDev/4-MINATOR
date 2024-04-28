using FourMinator.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;





var builder = WebApplication.CreateBuilder(args);


var connectionString = "server=localhost;port=3306;database=fourminator;user=fmadm;password=4minatorDev24";
var serverVersion = MySqlServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContext<AuthContext>(options => options.UseMySql(connectionString, serverVersion));


var app = builder.Build();

if (args.FirstOrDefault() is { } arg && arg.StartsWith("newIdentityProvider="))
{
    var identityProviderName = arg.Split('=')[1];
    var dbContext = app.Services.GetRequiredService<AuthContext>();
    var identityProviderRepo = new IdentityProviderRepository(dbContext);
    var newAuthenticator = new IdentityProviderAuthenticator(identityProviderRepo);

    newAuthenticator.CreateIdentityProvider(identityProviderName);
    var authKey = newAuthenticator.GenerateAuthKey();
    newAuthenticator.SaveIdentityProvider();
    
    Console.WriteLine($"Identity Provider {identityProviderName} created with AuthKey: {authKey}");

    return;
}





app.Run();
