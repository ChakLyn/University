using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AILab_1.AI.Models;

namespace AILab_1.AI.Core
{
    public class InheritanceCore
    {
        private InheritanceTree _tree = new InheritanceTree();

        public void StartInheritanceCore()
        {
            bool exitFlag = true;
            Console.WriteLine("Inheritance core has started right now! You can work.\n");
            string elementNamePattern = "Current frame: {0}";
            InheritanceNodeModel treeElement = new InheritanceNodeModel();

            while (exitFlag)
            {
                Console.WriteLine(elementNamePattern, treeElement.Name);

                Console.Write("\nEnter the command: ");
                string choice = Console.ReadLine();

                switch (choice.ToLower())
                {
                    case "find":
                        {
                            Console.Write("Enter the name of the frame: ");
                            string findFrameName = Console.ReadLine();

                            InheritanceNodeModel bufTreeElement = _tree.Tree.Keys.FirstOrDefault(item => item.Name == findFrameName);

                            if (bufTreeElement != null)
                            {
                                treeElement = bufTreeElement;
                            }
                            else
                            {
                                Console.WriteLine($"I am soryy, but there is not such frame \"{findFrameName}\"");
                            }

                            break;
                        }
                    case "max":
                        {
                            var maxRoute = BuildVariantWay(presentationCollection.FirstOrDefault().Key, SelectRouteEnum.ByMax);
                            Console.WriteLine(ComposerFormat, maxRoute.Path, maxRoute.Cost);
                            break;
                        }
                    case "min":
                        {
                            var minRoute = BuildVariantWay(presentationCollection.FirstOrDefault().Key, SelectRouteEnum.ByMin);
                            Console.WriteLine(ComposerFormat, minRoute.Path, minRoute.Cost);
                            break;
                        }
                    case "exit":
                        {
                            exitFlag = false;
                            break;
                        }
                    case "help":
                        {
                            Console.WriteLine("find {name} command - find and set the frame.");
                            Console.WriteLine("info - show info for the frame");
                            Console.WriteLine("all info - show all info based on the inheritance");
                            Console.WriteLine("add atr - add new atribute for the frame");
                            Console.WriteLine("add son - add new frame foe current frame");
                            Console.WriteLine("set atr {name} - set some value for the attribute with name");
                            Console.WriteLine("delete atr {name} - remove delete attribute from the frame");
                            Console.WriteLine("delete - remove current frame");
                            Console.WriteLine("exit - exit program");
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

        private InheritanceNodeModel CreateNode(InheritanceNodeModel parent = null)
        {
            Console.WriteLine("Enter new object name: ");
            string nodeName = Console.ReadLine();

            InheritanceNodeModel treeNode = new InheritanceNodeModel { Name = nodeName };

            if (parent != null)
            {
                treeNode.Parent = parent.Name;
                treeNode.Fields = parent.Fields;
            }
            treeNode.Fields.Union(EnterAttributes());

            return treeNode;
        }

        private Dictionary<string, string> EnterAttributes()
        {
            Console.WriteLine("Add new attributes");
            string newAttributes = Console.ReadLine();

            List<string> newAttributesList = new List<string>(newAttributes.Split(' '));
            return newAttributesList.ToDictionary(key => key, value =>
            {
                Console.Write($"{value}: ");
                return Console.ReadLine();
            });
        }
    }
}
