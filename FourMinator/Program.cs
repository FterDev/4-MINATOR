using FourMinator.Auth;
using FourMinator.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;






var builder = WebApplication.CreateBuilder(args);

var auth0domain = $"https://{builder.Configuration["Auth0:Domain"]}/";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = auth0domain;
    options.Audience = builder.Configuration["Auth0:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = ClaimTypes.NameIdentifier
    };
});


builder.Services.AddDbContext<FourminatorContext>();
builder.Services.AddControllers();
builder.Services.AddScoped<IIdentityProviderAuthenticator, IdentityProviderAuthenticator>( x => new IdentityProviderAuthenticator(new IdentityProviderRepository(x.GetRequiredService<FourminatorContext>())));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<FourminatorContext>();

    dbContext.Database.Migrate();
}


if (args.FirstOrDefault() is { } arg && arg.StartsWith("newIdentityProvider="))
{
    var identityProviderName = arg.Split('=')[1];
    var newAuthenticator = new IdentityProviderAuthenticator(new IdentityProviderRepository(app.Services.GetRequiredService<FourminatorContext>()));
    newAuthenticator.CreateIdentityProvider(identityProviderName);
    var authKey = newAuthenticator.GenerateAuthKey();
    newAuthenticator.SaveIdentityProvider();
    
    Console.WriteLine($"Identity Provider {identityProviderName} created with AuthKey: {authKey}");
    return;
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseAuthorization();
app.UseAuthentication();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



app.Run();
