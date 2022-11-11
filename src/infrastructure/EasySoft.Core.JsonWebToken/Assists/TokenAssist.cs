namespace EasySoft.Core.JsonWebToken.Assists;

public static class TokenAssist
{
    public static ClaimsPrincipal ValidateToken(string token)
    {
        return new JwtSecurityTokenHandler().ValidateToken(
            token,
            GenerateTokenValidationParameters(),
            out _
        );
    }

    public static TokenValidationParameters GenerateTokenValidationParameters()
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = GeneralConfigAssist.GetJsonWebTokenValidateIssuer(),
            ValidIssuer = GeneralConfigAssist.GetJsonWebTokenValidIssuer(),
            ValidateAudience = GeneralConfigAssist.GetJsonWebTokenValidateAudience(),
            ValidAudience = GeneralConfigAssist.GetJsonWebTokenValidAudience(),
            ValidateLifetime = GeneralConfigAssist.GetJsonWebTokenValidateLifetime(),
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(GeneralConfigAssist.GetJsonWebTokenIssuerSigningKey())
            )
        };
    }

    public static string GenerateJsonWebToken(string identification, params Claim[] claims)
    {
        var claimsAdjust = new List<Claim>
        {
            new(JwtRegisteredClaimSpecialNames.EasySoftTokenIdentity, identification)
        };

        claimsAdjust.AddRange(claims);

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                GeneralConfigAssist.GetJsonWebTokenIssuerSigningKey()
            )
        );

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            GeneralConfigAssist.GetJsonWebTokenValidIssuer(),
            GeneralConfigAssist.GetJsonWebTokenValidAudience(),
            claimsAdjust,
            expires: DateTime.Now.AddSeconds(DynamicConfigAssist.GetTokenExpires()),
            signingCredentials: credentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        if (!GeneralConfigAssist.GetTokenServerDumpSwitch()) return token;

        var asyncCacheOperator = AutofacAssist.Instance.Resolve<IAsyncCacheOperator>();

        var cacheKey = Guid.NewGuid().ToString().Remove("-").ToLower();

        asyncCacheOperator.SetAsync(
            cacheKey,
            token,
            new TimeSpan(TimeSpan.TicksPerSecond * DynamicConfigAssist.GetTokenExpires())
        );

        return cacheKey;
    }
}