using IdentityServer4.Models;

namespace IdentityServerAuth
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
        {
            new ApiScope("api")
        };
        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };
        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "cc",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes =
                {
                    "api"
                }
            },
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("secret".Sha256()) },
                RedirectUris = { "https://localhost:7183/signin-oidc" },// where to redirect to after login
                PostLogoutRedirectUris = { "https://localhost:7183/signout-callback-oidc" },// where to redirect to after logout
                AllowedGrantTypes = GrantTypes.Code,
                RequireConsent = false,
                AllowedScopes =
                {
                    "api"
                }
            },
             new Client
            {
                ClientId = "user",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                RequireConsent = false,
                AllowOfflineAccess = true,
                AllowedScopes =
                {
                    "offline_access",
                    "api"
                }
            }
        };
    }
}
