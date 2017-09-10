using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AILab_1.AI.Models;
using AILab_1.Utils.JsonHelper;
using AILab_1.AI.Core;
using AILab_1.Utils.Randomizer;

namespace AILab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            DialogPhrases phrases = JsonSerializer.DeserializeDialogPhrases(GetJson("Resources\\Phrases.json"));
            Dictionary<int, double> dictionaryRandomTable =
                JsonSerializer.DeserializeDiscreteRandomDictionary(GetJson("Resources\\DiscreteRandomTable.json"));

            DialogCore dialogCore = new DialogCore(phrases, dictionaryRandomTable);

            dialogCore.StartDialog();
        }

        static string GetJson(string path)
        {
           return File.ReadAllText(path);
        }
    }
}
