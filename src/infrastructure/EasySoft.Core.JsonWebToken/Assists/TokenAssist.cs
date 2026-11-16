using EasySoft.Core.AuthenticationCore.Assists;
using EasySoft.Core.Infrastructure.Assists;

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
        var authenticationConfigureAssist = ServiceProviderAssist.Resolve<AuthenticationConfigureAssist>();

        return new TokenValidationParameters
        {
            ValidateIssuer = authenticationConfigureAssist.GetJsonWebTokenValidateIssuer().Value,
            ValidIssuer = authenticationConfigureAssist.GetJsonWebTokenValidIssuer().Value,
            ValidateAudience = authenticationConfigureAssist.GetJsonWebTokenValidateAudience().Value,
            ValidAudience = authenticationConfigureAssist.GetJsonWebTokenValidAudience().Value,
            ValidateLifetime = authenticationConfigureAssist.GetJsonWebTokenValidateLifetime().Value,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(authenticationConfigureAssist.GetJsonWebTokenIssuerSigningKey().Value)
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

        var authenticationConfigureAssist = ServiceProviderAssist.Resolve<AuthenticationConfigureAssist>();

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                authenticationConfigureAssist.GetJsonWebTokenIssuerSigningKey().Value
            )
        );

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            authenticationConfigureAssist.GetJsonWebTokenValidIssuer().Value,
            authenticationConfigureAssist.GetJsonWebTokenValidAudience().Value,
            claimsAdjust,
            expires: DateTime.Now.AddSeconds(authenticationConfigureAssist.GetTokenExpires().Value),
            signingCredentials: credentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        if (!authenticationConfigureAssist.GetTokenServerDumpSwitch().Value)
        {
            return token;
        }

        var cacheOperator = ServiceProviderAssist.Resolve<ICacheOperator>();

        var cacheKey = Guid.NewGuid().ToString().Remove("-").ToLower();

        cacheOperator.Set(
            cacheKey,
            token,
            new TimeSpan(TimeSpan.TicksPerSecond * authenticationConfigureAssist.GetTokenExpires().Value)
        );

        return cacheKey;
    }
}