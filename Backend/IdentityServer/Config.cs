using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> GetIdentityResources() =>
      new List<IdentityResource>
      {
          new IdentityResources.OpenId(),
          new IdentityResources.Profile(),
          new IdentityResource("roles", "User role(s)", new List<string> { "role" })
      };

    public static IEnumerable<ApiScope> GetApiScopes() =>
        new List<ApiScope> { new ApiScope("vizsgaremekAPI", "VizsgaremekIT API") };

    public static IEnumerable<ApiResource> GetApiResources() =>
    new List<ApiResource>
    {
        new ApiResource("vizsgaremekAPI", "VizsgaremekIT API")
        {
            Scopes = new List<string> { "vizsgaremekAPI" }
        }
    };

    public static IEnumerable<Client> GetClients() =>
        new List<Client>
        {
            new Client
            {
                ClientId = "vizsgaremekClient",
                ClientName = "Vizsgaremek Client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                ClientSecrets = { new Secret("mysecret".Sha256()) },
                AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, "vizsgaremekAPI" },
            },
            new Client
            {
                ClientName = "Vizsgaremek Angular Client",
                ClientId = "vizsgaremek-angular-client",
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = new List<string>{ "http://localhost:4200/signin-callback", "http://localhost:4200/assets/silent-callback.html" },
                RequirePkce = true,
                AllowAccessTokensViaBrowser = true,
                AllowedScopes = 
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "vizsgaremekAPI"
                },
                AllowedCorsOrigins = { "http://localhost:4200" },
                RequireClientSecret = false,
                ClientSecrets = { new Secret("mysecret".Sha512()) },
                PostLogoutRedirectUris = new List<string> { "http://localhost:4200/signout-callback" },
                RequireConsent = false,
                AccessTokenLifetime = 600
            },
        };
    public static List<TestUser> GetUsers() =>
          new List<TestUser>
          {
              new TestUser
              {
                  SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b4",
                  Username = "Mick",
                  Password = "MickPassword",
                  Claims = new List<Claim>
                  {
                      new Claim("given_name", "Mick"),
                      new Claim("family_name", "Mining"),
                      new Claim("role", "Administrator")
                  }
              },
              new TestUser
              {
                  SubjectId = "c95ddb8c-79ec-488a-a485-fe57a1462340",
                  Username = "Jane",
                  Password = "JanePassword",
                  Claims = new List<Claim>
                  {
                      new Claim("given_name", "Jane"),
                      new Claim("family_name", "Downing"),
                      new Claim("role", "Member")
                  }
              }
          };
}
