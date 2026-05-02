using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityService.application.Common
{
    public class RefreshToken
    {
        public string TokenValue { get; set; }
        public DateTime Expires { get; set; }
    }
}
