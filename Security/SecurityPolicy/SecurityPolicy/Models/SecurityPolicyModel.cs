using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityPolicy.Models
{
    public class SecurityPolicyModel
    {
        public List<UserModel> Users { get; set; }
        public List<string> Rights { get; set; }
        public List<string> Objects { get; set; }
    }
}
