using FourMinator.Auth;
using FourMinator.Persistence;
using FourMinator.RobotServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;






var builder = WebApplication.CreateBuilder(args);

var auth0domain = $"https://{builder.Configuration["Auth0:Domain"]}/";


builder.Services.AddDbContext<FourminatorContext>();

builder.Services.AddScoped<IIdentityProviderAuthenticator, IdentityProviderAuthenticator>( x => new IdentityProviderAuthenticator(new IdentityProviderRepository(x.GetRequiredService<FourminatorContext>())));
builder.Services.AddScoped(x => new RobotService(new RobotRepository(x.GetRequiredService<FourminatorContext>()), new UserRepository(x.GetRequiredService<FourminatorContext>())));
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen( c =>{

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "4-MINATOR API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });

});

builder.Services.AddControllers();

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




app.UseRouting();

// Use authentication middleware
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



app.Run();
