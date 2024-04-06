using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Messaging.Api;

public static class Auth
{
    public static string MintJWTForUser()
    {
        var keyBytes = Encoding.ASCII.GetBytes(Env.JWTPrivateKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            Issuer = Env.JWTIssuer,
            Audience = Env.JWTAudience,
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(keyBytes),
            SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

        return jwtToken;
    }
}