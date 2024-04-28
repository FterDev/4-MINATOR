using Fourminator.Auth;





var builder = WebApplication.CreateBuilder(args);


if (args.FirstOrDefault() is { } arg && arg.StartsWith("newIdentityProvider="))
{
    var identityProviderName = arg.Split('=')[1];
    var identityProviderRepo = new IdentityProviderRepository();
    var newAuthenticator = new IdentityProviderAuthenticator(identityProviderRepo);
    
}

var app = builder.Build();



app.Run();
