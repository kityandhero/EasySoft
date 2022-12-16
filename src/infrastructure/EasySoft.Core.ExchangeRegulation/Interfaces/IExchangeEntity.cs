﻿namespace EasySoft.Core.ExchangeRegulation.Interfaces;

/// <summary>
/// IExchangeEntity
/// </summary>
public interface IExchangeEntity
{
    /// <summary>
    /// GetId
    /// </summary>
    /// <returns></returns>
    public string GetId();

    /// <summary>
    /// GetIdentificationName
    /// </summary>
    /// <returns></returns>
    public string GetIdentificationName();
}