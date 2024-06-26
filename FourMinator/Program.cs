using FirebaseAdmin;
using FourMinator.Auth;
using FourMinator.Persistence;
using FourMinator.RobotServices;
using FourMinator.RobotServices.Hubs;
using Google.Apis.Auth.OAuth2;
using Microsoft.OpenApi.Models;
using HiveMQtt;
using HiveMQtt.Client;
using HiveMQtt.Client.Options;
using FourMinator.RobotServices.Services;
using FourMinator.GameServices.Hubs;
using FourMinator.AuthServices.Middleware;
using FourMinator.GameServices.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using FourMinator.BotLogic;








var builder = WebApplication.CreateBuilder(args);
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"FirebaseConfig.json");

string openingBookPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "7x6.book");
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));


builder.Services.AddDbContext<FourminatorContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>( x => new UserRepository(x.GetRequiredService<FourminatorContext>()));
builder.Services.AddScoped<IIdentityProviderAuthenticator, IdentityProviderAuthenticator>( x => new IdentityProviderAuthenticator(new IdentityProviderRepository(x.GetRequiredService<FourminatorContext>())));
builder.Services.AddScoped<ILobbyService, LobbyService>();
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddSingleton<ICollection<IGameBoard>>(new List<IGameBoard>());
builder.Services.AddSingleton<OpeningBook>(o => new OpeningBook(Position.WIDTH, Position.HEIGHT, openingBookPath));
builder.Services.AddScoped<Solver>(s => new Solver( s.GetRequiredService<OpeningBook>()));



string broker = "int.mqtt.4-minator.ch";
int port = 1883;
string clientId = "4MINATORSVC01";
string username = "test";
string password = "it.test99";


var mqttClientOptions = new HiveMQClientOptions
{
    Host = broker,
    Port = port,
    ClientId = clientId,
    UserName = username,
    Password = password
};


builder.Services.AddSingleton<IHiveMQClient>(new HiveMQClient(mqttClientOptions));
builder.Services.AddSingleton<MqttClientService>();
builder.Services.AddHostedService<MqttBackgroundService>();



builder.Services.AddScoped<IRobotService, RobotService>();
builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.GetApplicationDefault()
}));

builder.Services.AddSingleton<FireAuth>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.Authority = "https://securetoken.google.com/fourminator-zbw";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidAudience = "fourminator-zbw",
            ValidIssuer = "https://securetoken.google.com/fourminator-zbw",
            ValidateIssuerSigningKey = false,
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Query["access_token"];
                return Task.CompletedTask;
            }
        };
    });


builder.Services.AddSignalR();
builder.Services.AddHttpClient();
builder.Services.AddCors();



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


app.UseSerilogRequestLogging();

app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .SetIsOriginAllowed(origin => true));

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<RobotsHub>("/robotsHub");
app.MapHub<LobbyHub>("/lobbyHub");
app.MapHub<MatchHub>("/matchHub");






app.Run();
