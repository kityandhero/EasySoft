﻿using EasySoft.Core.PermissionServer.Core.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.PermissionServer.Core.Subscribers;

/// <summary>
/// CapEventSubscriber
/// </summary>
public sealed class AccessWayExchangeSubscriber : ICapSubscribe
{
    private readonly ILogger<AccessWayExchangeSubscriber> _logger;
    private readonly IWebHostEnvironment _environment;
    private readonly ISecurityService _securityService;

    public AccessWayExchangeSubscriber(
        ILogger<AccessWayExchangeSubscriber> logger,
        IWebHostEnvironment environment,
        ISecurityService securityService
    )
    {
        _logger = logger;
        _environment = environment;

        _securityService = securityService;
    }

    /// <summary>
    /// 订阅充值事件
    /// </summary>
    /// <param name="accessWayExchange"></param>
    /// <returns></returns>
    [CapSubscribe(TransmitterTopic.AccessWayExchange)]
    public async Task Process(AccessWayExchange accessWayExchange)
    {
        if (_environment.IsDevelopment())
        {
            _logger.LogAdvanceExecute($"{GetType().Name}.{nameof(Process)}");

            _logger.LogAdvancePrompt(
                $"Save AccessWayExchange -> {accessWayExchange.BuildInfo()}."
            );
        }

        await _securityService.SaveAccessWayModelAsync(accessWayExchange);
    }
}