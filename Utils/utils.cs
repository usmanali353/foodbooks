using foodbooks.Models;
using foodbooks.ViewModels;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit.Text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace foodbooks.Utils
{
    public class utils
    {
        public static void sendMail(string to,string VerificationLink,string message,string subject)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("FoodBooks"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = "<h2>"+message+"</h2>" + @"<Button style=""background-color:blue;padding: 14px 25px;border:none;"">" + @"<a style=""text-decoration:none;color: white;"" href="+@""+VerificationLink+""+">Verify</a></Button>" };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("iib.tech357@gmail.com", "iibtech@123");
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public static string GenerateAccessToken(string userId, ApplicationUser user,IList<string> RolesList)

        {

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("SurveyAppjwtTokenforauthorization"));
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
            foreach (var userRole in RolesList)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var token = new JwtSecurityToken(
                    issuer: "SurveyApp",
                    audience: "SurveyApp",
                    expires: DateTime.Now.AddHours(3),
                    claims:authClaims,
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                    );
            token.Payload["UserInfo"] = new {id =userId,name=user.name,email=user.Email,phone=user.PhoneNumber,city=user.city,country=user.country };

            return tokenHandler.WriteToken(token);

        }

        public static List<Dropdown> getQuestionTypes() 
        {
            return new List<Dropdown> 
            {
                new Dropdown{id=1,name="Star Rating"},
                new Dropdown{id=2,name="Yes/No" },
                new Dropdown{id=3,name ="Dropdown" },
                new Dropdown{id=4,name="Numbering" }
            };
        }

        public static UserInfo ParseToken(string token) 
        {
            var stream = token;
            var handler = new JwtSecurityTokenHandler();
           // var jsonToken = handler.ReadToken(stream);
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            TokenPayLoad payload = JsonConvert.DeserializeObject<TokenPayLoad>(tokenS.Payload.SerializeToJson());

            return payload.userInfo;
        }

    }
}
