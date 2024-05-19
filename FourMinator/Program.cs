using FirebaseAdmin;
using FourMinator.Auth;
using FourMinator.Persistence;
using FourMinator.RobotServices;

using FourMinator.RobotServices.Hubs;
using FourMinator.RobotServices.MqttControllers;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MQTTnet;
using MQTTnet.AspNetCore;
using MQTTnet.Server;







var builder = WebApplication.CreateBuilder(args);
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"FirebaseConfig.json");

builder.Services.AddSignalR();

builder.Services.AddHttpClient();
builder.Services.AddSingleton<FourminatorContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>( x => new UserRepository(x.GetRequiredService<FourminatorContext>()));
builder.Services.AddScoped<IIdentityProviderAuthenticator, IdentityProviderAuthenticator>( x => new IdentityProviderAuthenticator(new IdentityProviderRepository(x.GetRequiredService<FourminatorContext>())));



builder.Services.AddSingleton<IRobotService, RobotService>();


builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.GetApplicationDefault()
}));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<MqttRobotController>();



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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<RobotsHub>("/robotsHub");






app.Run();
