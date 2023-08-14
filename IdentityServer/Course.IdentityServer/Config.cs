// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Course.IdentityServer
{
    public static class Config
    {
        private const string _resource_catalog = "resource_catalog";
        private const string _photo_stock_catalog = "photo_stock_catalog";
        private const string _catalog_fullpermission = "catalog_fullpermission";
        private const string _photo_stock_fullpermission = "photo_stock_fullpermission";


        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource(_resource_catalog){Scopes={_catalog_fullpermission }},
            new ApiResource(_photo_stock_catalog){Scopes={ _photo_stock_fullpermission }},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.Email(),
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource(){Name="roles",DisplayName="Roles",Description="User Rolles", UserClaims=new[]{"role" } }
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope(_catalog_fullpermission,"Catalog api tam erişim"),
            new ApiScope(_photo_stock_fullpermission,"Foto stok api tam erişim"),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<Client> Clients => new Client[]
        {
            new Client{
                ClientName="Microservice Project",
                ClientId ="Client",
                ClientSecrets={new Secret("secret".Sha256())},
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes={
                    _catalog_fullpermission,
                    _photo_stock_fullpermission,
                    IdentityServerConstants.LocalApi.ScopeName
                }
            },
            new Client{
                ClientName="Microservice Project",
                ClientId ="ClientForUser",
                AllowOfflineAccess = true,
                ClientSecrets={new Secret("secret".Sha256())},
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes={
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    IdentityServerConstants.LocalApi.ScopeName,
                    "roles"
                },
                AccessTokenLifetime=1*60*60,
                RefreshTokenExpiration=TokenExpiration.Absolute,
                AbsoluteRefreshTokenLifetime =(int) (DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                RefreshTokenUsage=TokenUsage.ReUse
            },
        };
    }
}