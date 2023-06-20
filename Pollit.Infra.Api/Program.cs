using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Pollit.Application._Ports;
using Pollit.Application.Auth.SendResetPasswordLinkEmailToUser;
using Pollit.Application.Comments.GetCommentsOfAPoll;
using Pollit.Application.Polls.GetPollFeed;
using Pollit.Application.Users.SendEmailConfirmationEmailToUser;
using Pollit.Domain._Ports;
using Pollit.Domain.Comments;
using Pollit.Domain.Comments._Ports;
using Pollit.Domain.Polls._Ports;
using Pollit.Domain.Users._Ports;
using Pollit.Domain.Users.Events;
using Pollit.Domain.Users.Services;
using Pollit.Infra.Api;
using Pollit.Infra.Api.AuthenticatedUserProviders;
using Pollit.Infra.EfCore.NpgSql;
using Pollit.Infra.EfCore.NpgSql.Projections.Comments;
using Pollit.Infra.EfCore.NpgSql.Projections.Polls;
using Pollit.Infra.EfCore.NpgSql.Repositories.Comments;
using Pollit.Infra.EfCore.NpgSql.Repositories.Polls;
using Pollit.Infra.EfCore.NpgSql.Repositories.Users;
using Pollit.Infra.FrontApp.UrlBuilder;
using Pollit.Infra.FrontApp.UrlBuilder.Config;
using Pollit.Infra.GoogleApi;
using Pollit.Infra.Jwt;
using Pollit.Infra.Mailing.Mailjet;
using Pollit.Infra.PasswordEncryptor;
using Pollit.SeedWork;
using Pollit.SeedWork.Eventing;

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
    .AddScoped<IPollRepository, PollRepository>()
    .AddScoped<IPollFeedProjection, PollFeedProjection>()
    .AddScoped<ICommentRepository, CommentRepository>()
    .AddScoped<IGetCommentsOfAPollProjection, GetCommentsOfAPollProjection>()
    .AddTransient<IUnitOfWork, UnitOfWork>()
    .AddSingleton<DomainEventBus>()
    .AddSingleton<IAccessTokenManager, AccessTokenManager>()
    .AddSingleton<IPasswordEncryptor, PasswordEncryptor>()
    .AddSingleton<IGoogleAuthenticator, GoogleAuthenticator>()
    .AddTransient<IDateTimeProvider, DateTimeProvider>()
    .AddTransient<IEmailVerificationEmailSender, EmailSender>()
    .AddTransient<IFrontAppUrlBuilder, FrontAppUrlBuilder>()
    .BindConfigurationSectionAsSingleton<JwtConfig>(configuration.GetSection("JwtConfig"), out var jwtConfig)
    .BindConfigurationSectionAsSingleton<GoogleAuthenticatorConfig>(configuration.GetSection("Google"))
    .BindConfigurationSectionAsSingleton<MailjetConfig>(configuration.GetSection("Mailjet"))
    .BindConfigurationSectionAsSingleton<FrontAppConfig>(configuration.GetSection("FrontApp"))
    .AddTransient<CredentialsAuthenticationService>()
    .AddTransient<RefreshTokenAuthenticationService>()
    .AddTransient<GoogleAuthenticationService>()
    .AddTransient<AccountSettingsService>()
    .AddTransient<PollCommentingService>()
    .AddQueryAndCommandHandlers()
    .AddDomainEventHandlers()
    .AddJwtAuthentication(jwtConfig)
    .AddAuthorizationPolicies()
    .AddHttpContextAccessor()
    .AddTransient<IAuthenticatedUserProvider, HttpContextAuthenticatedUserProvider>();

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

app.RegisterDomainEventHandler<UserCreatedEvent, SendEmailConfirmationToUserEventConsumer>();
app.RegisterDomainEventHandler<ResetPasswordLinkCreatedEvent, SendResetPasswordLinkEmailToUserEventConsumer>();

app.ApplyDbMigrations();

app.Run();