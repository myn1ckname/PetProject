﻿using Microsoft.AspNetCore.Mvc;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores.Serialization;

namespace PetProject.IdentityServer.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IPersistedGrantService _persistedGrantService;
    private readonly IRefreshTokenService _refreshTokenService;

    private readonly IPersistedGrantStore _store;
    private readonly IPersistentGrantSerializer _serializer;
    public HomeController(
        ILogger<HomeController> logger,
        IPersistedGrantService persistedGrantService,
        IRefreshTokenService refreshTokenService,
        IPersistedGrantStore store,
        IPersistentGrantSerializer serializer)
    {
        _logger = logger;
        _persistedGrantService = persistedGrantService;
        _refreshTokenService = refreshTokenService;
        _store = store;
        _serializer = serializer;
    }

    [HttpGet]
    public async Task<IActionResult> GetAync()
    {
        // var grants = await _persistedGrantService.GetAllGrantsAsync("7b3088c8-5192-4edd-b028-fed8afbc72fe");

        var subjectId = "7b3088c8-5192-4edd-b028-fed8afbc72fe";
        var grants = (await _store.GetAllAsync(new PersistedGrantFilter { SubjectId = subjectId })).ToArray();

        var refreshes = grants
            .Where(x => x.Type == IdentityServerConstants.PersistedGrantTypes.RefreshToken)
            .Select(x => _serializer.Deserialize<RefreshToken>(x.Data))
            .Select(x => new Grant
            {
                ClientId = x.ClientId,
                SubjectId = subjectId,
                Description = x.Description,
                Scopes = x.AuthorizedScopes,
                CreationTime = x.CreationTime,
                Expiration = x.CreationTime.AddSeconds(x.Lifetime)
            });

        if (refreshes.Any())
        {
            var refresh = refreshes.First();
        }

        return Ok();
    }
}