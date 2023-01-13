using Microsoft.EntityFrameworkCore;
using Pollit.Application._Ports;
using Pollit.Application.Auth.SigninWithGoogle;
using Pollit.Application.Auth.SignupWithCredentials;
using Pollit.Domain.Users;
using Pollit.Infra.EfCore.NpgSql;
using Pollit.Infra.EfCore.NpgSql.Repositories.Users;
using Pollit.Infra.GoogleApi;
using Pollit.Infra.Jwt;
using Pollit.Infra.PasswordEncryptor;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

services
    .AddEndpointsApiExplorer()
    .AddLogging(logging => logging.AddConsole())
    .AddControllers();

services.AddSwaggerGen();

services.AddDbContext<PollitDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("Postgres")));

services
    .AddScoped<IUserRepository, UserRepository>()
    .AddTransient<IUnitOfWork, UnitOfWork>()
    .AddTransient<SignupWithCredentialsCommandHandler>()
    .AddTransient<SigninWithGoogleCommandHandler>()
    .AddSingleton<IAccessTokenManager, AccessTokenManager>()
    .AddSingleton<IPasswordEncryptor, PasswordEncryptor>()
    .AddSingleton<IGoogleAuthenticator, GoogleAuthenticator>();

var jwtConfig = new JwtConfig();
configuration.GetSection("JwtConfig").Bind(jwtConfig);
services.AddSingleton<JwtConfig>(_ => jwtConfig);

var googleAuthenticatorConfig = new GoogleAuthenticatorConfig();
configuration.GetSection("Google").Bind(googleAuthenticatorConfig);
services.AddSingleton<GoogleAuthenticatorConfig>(_ => googleAuthenticatorConfig);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policyBuilder => policyBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().WithExposedHeaders("*"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PollitDbContext>();
    db.Database.Migrate();
}

app.Run();