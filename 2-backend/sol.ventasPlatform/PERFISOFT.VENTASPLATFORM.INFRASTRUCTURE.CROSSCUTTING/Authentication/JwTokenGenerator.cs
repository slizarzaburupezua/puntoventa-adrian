using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PERFISOFT.VENTASPLATFORM.APPLICATION.COMMON;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.CROSSCUTTING.Authentication
{
    public class JwTokenGenerator : IJwTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateTokenSignIn(Guid idUsuarioGuid, string nombres, string apellidos, string correo, int idRol, string nombreRol, int idMoneda, string idFoto, string urlfoto)
        {
            byte[] secretKey = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWSettings:SecretKey"));

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(secretKey),
                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("idUsuario", idUsuarioGuid.ToString()),
                new Claim("nombres", nombres),
                new Claim("apellidos", apellidos),
                new Claim("correo", correo),
                new Claim("idRol", idRol.ToString()),
                new Claim("nombreRol", nombreRol),
                new Claim("idMoneda", idMoneda.ToString()),
                new Claim("idFoto", idFoto ?? string.Empty),
                new Claim("urlfoto", urlfoto ?? string.Empty)
            };

            var securityToken = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("JWSettings:Issuer"),
                audience: _configuration.GetValue<string>("JWSettings:Audience"),
                expires: DateTime.Now.AddHours(_configuration.GetValue<int>("JWSettings:ExpiryHours")),
                claims: claims,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }


    }
}
