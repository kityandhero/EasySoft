﻿namespace EasySoft.Core.AuthenticationCore.Entities;

public class Token
{
    /// <summary>
    /// 身份标识, 请确保范围内唯一
    /// </summary>
    public string IdentificationCode { get; set; }

    public Token()
    {
        IdentificationCode = "";
    }
}