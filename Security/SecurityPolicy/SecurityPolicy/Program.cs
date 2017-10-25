using System;
using SecurityPolicy.Utils.JsonHelper;
using SecurityPolicy.Models;
using SecurityPolicy.Core;

namespace SecurityPolicy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SecurityPolicyModel securityPolicyModel = JsonSerializer.DeserializeSecurityPolicyModel(
                JsonSerializer.GetJson("Resources\\SecurityPolicyConfig.json"));

            SecurityPolicyImitation core = new SecurityPolicyImitation(securityPolicyModel);
            core.StartImitationSystem();
        }
    }
}
