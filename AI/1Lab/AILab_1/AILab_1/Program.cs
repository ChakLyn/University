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
            bool exitFlag = true;
            Console.WriteLine("AI have started");
            while (exitFlag)
            {
                Console.Write("Enter the command: ");
                string choice = Console.ReadLine();
                switch (choice.ToLower())
                {
                    case "dialog":
                        {
                            DialogPhrases phrases = JsonSerializer.DeserializeDialogPhrases(GetJson("Resources\\Phrases.json"));
                            Dictionary<int, double> dictionaryRandomTable =
                                JsonSerializer.DeserializeDiscreteRandomDictionary(GetJson("Resources\\DiscreteRandomTable.json"));

                            DialogCore dialogCore = new DialogCore(phrases, dictionaryRandomTable);

                            dialogCore.StartDialog();
                            break;
                        }
                    case "tree":
                        {
                            InheritanceTree tree = JsonSerializer.DeserializeInheritanceTree(GetJson("Resources\\InheritanceTree.json"));
                            InheritanceCore core = new InheritanceCore(tree);
                            core.StartInheritanceCore();
                            break;
                        }
                    case "composer":
                        {
                            try
                            {
                                Parts parts = JsonSerializer.DeserializeLinkerParts(GetJson("Resources\\ComposerList.json"));

                                ComposerCore composerCore = new ComposerCore(parts);
                                composerCore.StartCompose();
                            }
                            catch (FileNotFoundException)
                            {
                                Console.WriteLine("Sorry, but I have not found file!");
                            }
                            break;
                        }
                    case "exit":
                        {
                            exitFlag = false;
                            break;
                        }
                    case "help":
                        {
                            Console.WriteLine();
                            Console.WriteLine("Composer - starts composer core");
                            Console.WriteLine("Dialog - starts dialog core");
                            Console.WriteLine("Exit - exit program");
                            Console.WriteLine();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine($"I don't understand {choice} command.");
                            Console.WriteLine("You can use \"help\" command decription avaible commands.");
                            break;
                        }
                }
            }

        }

        static string GetJson(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
