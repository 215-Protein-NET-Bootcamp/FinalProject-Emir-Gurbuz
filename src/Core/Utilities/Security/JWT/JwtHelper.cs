using Core.Entity.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; private set; }
        private readonly AccessTokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("AccessTokenOptions").Get<AccessTokenOptions>();
        }
        public AccessToken CreateAccessToken(User user, List<string> roles)
        {
            _accessTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpiration);

            SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);

            JwtSecurityToken jwt = createJwtSecurityToken(user, roles, _accessTokenExpiration, _tokenOptions, signingCredentials);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            string token = tokenHandler.WriteToken(jwt);
            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        private JwtSecurityToken createJwtSecurityToken(User user, List<string> identityRoles, DateTime accessTokenExpiration, AccessTokenOptions tokenOptions, SigningCredentials signingCredentials)
        {
            return new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                notBefore: DateTime.UtcNow,
                expires: accessTokenExpiration,
                claims: setClaims(user, identityRoles),
                signingCredentials: signingCredentials);
        }

        private IEnumerable<Claim> setClaims(User user, List<string> roles)
        {
            List<Claim> claims = new List<Claim>();

            claims.AddEmail(user.Email);
            claims.AddName(String.Format("{0} {1}", user.FirstName, user.LastName));
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddDateOfBirth(user.DateOfBirth);
            claims.AddRoles(roles.ToArray());

            return claims;
        }
    }
}
