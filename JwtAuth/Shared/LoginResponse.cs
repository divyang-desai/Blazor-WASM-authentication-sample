using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.Shared
{
    public class LoginResponse
    {
        public int UserId { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public long ExpireAt { get; set; }
    }
}
