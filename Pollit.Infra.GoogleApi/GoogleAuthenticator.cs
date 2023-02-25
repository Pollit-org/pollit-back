using System;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Services;
using Pollit.Domain.Users;
using Google.Apis.PeopleService.v1;
using Google.Apis.PeopleService.v1.Data;
using Pollit.Domain._Ports;

namespace Pollit.Infra.GoogleApi;

public class GoogleAuthenticator : IGoogleAuthenticator
{
    private readonly GoogleAuthenticatorConfig _config;

    public GoogleAuthenticator(GoogleAuthenticatorConfig config)
    {
        _config = config;
    }

    public async Task<GoogleProfile> AuthenticateAsync(string code)
    {
        var authorizationCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
        {
            ClientSecrets = new ClientSecrets()
            {
                ClientId = _config.ClientId,
                ClientSecret = _config.ClientSecret
            }
        });

        var tokenResponse = await authorizationCodeFlow.ExchangeCodeForTokenAsync(
            UserId.NewUserId().ToString(), // ???
            code,
            "postmessage",
            CancellationToken.None);

        return await GetProfile(tokenResponse.AccessToken);
    }

    private async Task<GoogleProfile> GetProfile(string accessToken)
    {
        var userInfoAsync = GetUserInfoAsync(accessToken);
        var genderAndBirthdateAsync = GetGenderAndBirthdateAsync(accessToken);

        var userInfo = await userInfoAsync;
        var (gender, birthDate) = await genderAndBirthdateAsync;

        return new GoogleProfile()
        {
            Email = userInfo.Email,
            Gender = gender,
            BirthdayYear = birthDate?.Year,
            BirthdayMonth = birthDate?.Month,
            BirthdayDay = birthDate?.Day,
            GivenName = userInfo.GivenName,
            FamilyName = userInfo.FamilyName,
            VerifiedEmail = userInfo.VerifiedEmail,
            Locale = userInfo.Locale,
            Picture = userInfo.Picture
        };
    }

    private async Task<(string?, Date?)> GetGenderAndBirthdateAsync(string accessToken)
    {
        var service = new PeopleServiceService(new BaseClientService.Initializer
        {
            ApplicationName = "Pollit",
            ApiKey= _config.ApiKey,
        });

        var getRequest = service.People.Get("people/me");
        getRequest.PersonFields = "genders,birthdays";
        getRequest.AccessToken = accessToken;
        var result = await getRequest.ExecuteAsync();

        var gender = result.Genders?.FirstOrDefault()?.FormattedValue;
        var birthDate = result.Birthdays?.FirstOrDefault()?.Date;

        return (gender, birthDate);
    }

    private Task<Userinfo> GetUserInfoAsync(string accessToken)
    {
        var oauthSerivce = new Google.Apis.Oauth2.v2.Oauth2Service(
            new BaseClientService.Initializer()
            {
                ApplicationName = "Pollit",
                ApiKey = _config.ApiKey
            });

        var userInfoRequest = oauthSerivce.Userinfo.Get();
        userInfoRequest.OauthToken = accessToken;
            
        return userInfoRequest.ExecuteAsync();
    }
}