using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AILab_1.AI.Models;
using Newtonsoft.Json;

namespace AILab_1.Utils.JsonHelper
{
    public static class JsonSerializer
    {
        public static DialogPhrases DeserializeDialogPhrases(string jsonString)
        {
            DialogPhrases prases = JsonConvert.DeserializeObject<DialogPhrases>(jsonString);
            return prases;
        }

        public static Dictionary<int,double> DeserializeDiscreteRandomDictionary (string jsonString)
        {
            Dictionary<int, double> dictionary = JsonConvert.DeserializeObject<Dictionary<int, double>>(jsonString);
            return dictionary;
        }
    }
}
