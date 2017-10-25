using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SecurityPolicy.Models;
using Newtonsoft.Json;

namespace SecurityPolicy.Utils.JsonHelper
{
    public static class JsonSerializer
    {
        public static SecurityPolicyModel DeserializeSecurityPolicyModel(string jsonString)
        {
            return JsonConvert.DeserializeObject<SecurityPolicyModel>(jsonString);
        }

        public static string GetJson(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
