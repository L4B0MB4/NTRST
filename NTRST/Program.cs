using NTRST.DB.Auth.Authentication;
using NTRST.Extensions;
using NTRST.Middleware;
using NTRST.Models;
using NTRST.Models.Config;
using NTRST.Spotify.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>();

var authConfig = builder.Configuration.GetSection("auth").Get<AuthConfiguration>();
if(authConfig?.SigningSecret == null)throw new ArgumentNullException(nameof(authConfig.SigningSecret));

builder.Services.Configure<SsoConfiguration>(builder.Configuration.GetSection("sso"));
builder.Services.Configure<AuthConfiguration>(builder.Configuration.GetSection("auth"));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddMemoryCache();
builder.Services.AddSpotifyServices();
builder.Services.AddSpotifyHttpClient();
builder.Services.AddAuthDbContext();
builder.Services.AddTracksDbContext();
builder.Services.AddNTRSTAuthentication(authConfig.SigningSecret);


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseCors(x =>
{
    x.AllowAnyOrigin();
    x.AllowAnyMethod();
    x.AllowAnyHeader();
});
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<IdentityMiddleware>();
app.MapControllers();

app.MigrateAuthDatabase();
app.MigrateTracksDatabase();

app.Run();