using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Pollit.Domain._Ports;
using Pollit.Domain.Users;
using Pollit.Domain.Users.Services;
using Pollit.Infra.Api;
using Pollit.Infra.EfCore.NpgSql;
using Pollit.Infra.EfCore.NpgSql.Repositories.Users;
using Pollit.Infra.GoogleApi;
using Pollit.Infra.Jwt;
using Pollit.Infra.PasswordEncryptor;
using Pollit.SeedWork;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

services
    .AddEndpointsApiExplorer()
    .AddLogging(logging => logging.AddConsole())
    .AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

services.AddSwaggerGen();

services.AddDbContext<PollitDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("Postgres")));

services
    .AddScoped<IUserRepository, UserRepository>()
    .AddTransient<IUnitOfWork, UnitOfWork>()
    .AddSingleton<IAccessTokenManager, AccessTokenManager>()
    .AddSingleton<IPasswordEncryptor, PasswordEncryptor>()
    .AddSingleton<IGoogleAuthenticator, GoogleAuthenticator>()
    .AddTransient<IDateTimeProvider, DateTimeProvider>()
    .BindConfigurationSectionAsSingleton<JwtConfig>(configuration.GetSection("JwtConfig"), out var jwtConfig)
    .BindConfigurationSectionAsSingleton<GoogleAuthenticatorConfig>(configuration.GetSection("Google"))
    .AddTransient<CredentialsAuthenticationService>()
    .AddTransient<GoogleAuthenticationService>()
    .AddTransient<AccountSettingsService>()
    .AddQueryAndCommandHandlers()
    .AddJwtAuthentication(jwtConfig)
    .AddAuthorizationPolicies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policyBuilder => policyBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().WithExposedHeaders("*"));

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PollitDbContext>();
    db.Database.Migrate();
}

app.Run();