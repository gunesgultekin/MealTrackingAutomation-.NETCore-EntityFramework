using Azure;
using indasYemek.context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Text;

namespace indasYemek.Repositories
{
    public class userTableRepository
    {
        private readonly DBContext _context;
        public userTableRepository(DBContext context)
        {
            _context = context;
        }

        public String LoginAuth(string username, string password)
        {
            var user = _context.userTable.FirstOrDefault(u => u.username == username && u.password == password);
            if (user is null)
                return "ERROR"; // NO LOGIN
            else
            {
                const string secretKey = "SECRET_KEY_12345678910";
                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var tokenDescriber = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim("id", user.id.ToString()),
                            new Claim("username", username),
                            new Claim("role",user.role)
                        }),
                    Expires = DateTime.Now.AddDays(3),
                    SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var jwt = new JwtSecurityTokenHandler().CreateToken(tokenDescriber);
                var token = new JwtSecurityTokenHandler().WriteToken(jwt);
                return token;              
            }
        }
    }
}
