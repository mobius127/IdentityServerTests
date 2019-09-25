using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(), // claim estándar
                new IdentityResources.Profile(), // claim estándar
                new IdentityResources.Email(), // claim estándar
                new IdentityResource // claim personalizado
                {
                    Name = "Name",
                    UserClaims = new List<string> {"name"}
                },
                new IdentityResource // claim personalizado
                {
                    Name = "Surname",
                    UserClaims = new List<string> {"surname"}
                },
                new IdentityResource // claim personalizado
                {
                    Name = "WorkPlace",
                    UserClaims = new List<string> {"workplace"}
                }
           };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("ApiCoches", "Api de pruebas")
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "Javier",
                    Password = "Javier127"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "parrita",
                    Password = "thesame"
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "MySogeti",
                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    // scopes that client has access to
                    AllowedScopes = { "ApiCoches" }
                },
                new Client
                {
                    ClientId = "openIdSogeti",
                    ClientName = "openIdSogeti",
                    ClientSecrets = new List<Secret> { new Secret("superSecretPassword".Sha256()) },
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    AlwaysSendClientClaims=true,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "ApiCoches"
                    },
                    RedirectUris = new List<string> { "https://localhost:44338/signin-oidc" },
                    PostLogoutRedirectUris = new List<string> { "https://localhost:44338/signout-oidc" }
                 }
            };
        }
    }
}
