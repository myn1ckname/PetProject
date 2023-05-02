﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using PetProject.IdentityServer.Domain.Roles;

namespace PetProject.IdentityServer.DbSeeder;

internal static class SeedData
{
    public const string ADMIN_ROLE = "admin";
    public const string CUSTOMER_ROLE = "customer";

    public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Email(),
        new IdentityResources.Profile()
    };

    public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
    {
        // new ApiScope(name: "testscope", displayName: "Test API Scope"),
    };

    public static IEnumerable<Client> Clients => new List<Client>
    {              
        // external clien
        new Client
        {
            ClientId="ExternalClient",
            ClientSecrets= { new Secret("secret".Sha256())},
            AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
            AllowedScopes=new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email,
            }
        },
    };

    public static IEnumerable<ApplicationRole> Roles => new List<ApplicationRole>
    {
        new ApplicationRole(ADMIN_ROLE),
        new ApplicationRole(CUSTOMER_ROLE),
    };
}
