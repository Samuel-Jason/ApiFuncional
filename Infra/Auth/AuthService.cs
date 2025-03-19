using Microsoft.IdentityModel.Tokens; // Biblioteca para manipular tokens JWT e segurança
using System.IdentityModel.Tokens.Jwt; // Biblioteca para criar e manipular tokens JWT
using System.Security.Claims; // Biblioteca para definir e validar Claims (informações do usuário dentro do token)
using System.Security.Cryptography;
using System.Text; // Biblioteca para manipulação de strings, usada para converter a chave secreta em bytes

namespace ApiTesta.Infra.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration; // Interface para acessar as configurações do app (appsettings.json)

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration; // Injeta as configurações da aplicação
        }

        public string computedSha256Hash(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                // Converter bytes para string hexadecimal
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public string GenerateJwtToken(string email)
        {
            // Obtém as configurações do JWT do appsettings.json
            var issuer = _configuration["Jwt:Issuer"]; // Emissor do token (quem gera o token)
            var audience = _configuration["Jwt:Audience"]; // Público-alvo do token (quem pode usá-lo)
            var key = _configuration["Jwt:Key"]; // Chave secreta usada para assinar o token

            // Converte a chave secreta em bytes para ser usada na assinatura do token
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            // Define o algoritmo de assinatura do token (HMAC SHA-256 para garantir segurança)
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Define as Claims (informações do usuário que estarão no token)
            var claims = new List<Claim>
            {
                new Claim("username", email), // Claim personalizada para armazenar o e-mail do usuário
                //new Claim(ClaimTypes.Role, role) // Claim que define o papel do usuário (Admin, User, etc.)
            };

            // Cria o token JWT com os parâmetros configurados
            var token = new JwtSecurityToken(
                issuer: issuer, // Define quem está emitindo o token
                audience: audience, // Define quem pode usar o token
                expires: DateTime.UtcNow.AddSeconds(60), // Token válido por 20 segundos
                signingCredentials: credentials, // Adiciona as credenciais de assinatura ao token
                claims: claims // Adiciona as Claims ao token
            );

            // Converte o token JWT em uma string
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);

            // Retorna o token JWT em formato de string para ser enviado ao usuário
            return stringToken;
        }
    }
}
