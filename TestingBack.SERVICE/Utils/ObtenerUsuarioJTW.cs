using System.IdentityModel.Tokens.Jwt;

namespace TestingBack.SERVICE.Lib
{
    public class ObtenerUsuarioJWT
    {
        public string ObtenerUsuario(string authorizationHeader)
        {
            var handler = new JwtSecurityTokenHandler();
            string header = authorizationHeader;
            header = header.Replace("Bearer ", "");
            var token = handler.ReadJwtToken(header);
            var email = token.Claims.First(c => c.Type == "email").Value;
            return email;
        }
    }
}
