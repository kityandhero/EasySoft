using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EasySoft.Core.Config.ConfigAssist;
using Microsoft.IdentityModel.Tokens;

namespace EasySoft.Core.JsonWebToken.Assists;

public static class TokenAssist
{
    public static string GenerateJsonWebToken(string identification, params Claim[] claims)
    {
        var claimsAdjust = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, identification),
        };

        claimsAdjust.AddRange(claims);

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                GeneralConfigAssist.GetJsonWebTokenIssuerSigningKey()
            )
        );

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            issuer: GeneralConfigAssist.GetJsonWebTokenValidIssuer(),
            audience: GeneralConfigAssist.GetJsonWebTokenValidAudience(),
            claims: claimsAdjust,
            expires: DateTime.Now.AddSeconds(GeneralConfigAssist.GetTokenExpires()),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}