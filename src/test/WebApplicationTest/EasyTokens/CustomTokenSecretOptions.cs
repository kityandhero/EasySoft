﻿using EasySoft.Core.EasyToken.AccessControl;

namespace WebApplicationTest.EasyTokens;

/// <summary>
/// CustomTokenSecretOptions
/// </summary>
public class CustomTokenSecretOptions : ITokenSecretOptions
{
    /// <summary>
    /// GetKey
    /// </summary>
    /// <returns></returns>
    public string GetKey()
    {
        return "pqy7854skiosj7p4c74uiyo804tzecr8";
    }
}