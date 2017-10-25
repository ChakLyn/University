using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityPolicy.Models
{
    public class UserModel
    {
        public string IdUser { get; set; }
        public bool IsAdmin { get; set; }
        public Dictionary<string,List<string>> Rights { get; set; } = new Dictionary<string, List<string>>();
    }
}
