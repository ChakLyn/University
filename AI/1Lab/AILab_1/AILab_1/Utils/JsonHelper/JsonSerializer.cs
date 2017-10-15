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
            return JsonConvert.DeserializeObject<DialogPhrases>(jsonString);
        }

        public static Dictionary<int, double> DeserializeDiscreteRandomDictionary(string jsonString)
        {
            return JsonConvert.DeserializeObject<Dictionary<int, double>>(jsonString);
        }

        public static Parts DeserializeLinkerParts(string jsonString)
        {
            Parts parts = JsonConvert.DeserializeObject<Parts>(jsonString);
            return parts;
        }
    }
}
