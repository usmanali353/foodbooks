using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace foodbooks.ViewModels
{
    public class TokenPayLoad
    {
        public string HttpSchemasXmlsoapOrgWs200505IdentityClaimsName { get; set; }
        public string jti { get; set; }
        public string HttpSchemasMicrosoftComWs200806IdentityClaimsRole { get; set; }
        public int exp { get; set; }
        public string iss { get; set; }
        public string aud { get; set; }
        public UserInfo userInfo { get; set; }
    }
    public class UserInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string city { get; set; }
        public string country { get; set; }
    }
}
