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

            while (exitFlag)
            {
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
                                WorkWithNode(bufTreeElement);
                                bufTreeElement = null;
                            }
                            else
                            {
                                Console.WriteLine($"I am soryy, but there is not such frame \"{findFrameName}\"");
                            }

                            break;
                        }
                    case "set forall":
                        {
                            SetGlobalAttribute();
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
        
        private void WorkWithNode(InheritanceNodeModel treeElement)
        {
            bool exitFlag = true;
            string elementNamePattern = "Current frame: {0}";

            while (exitFlag)
            {
                Console.WriteLine(elementNamePattern, treeElement.Name);

                Console.Write("\nEnter the command: ");
                string choice = Console.ReadLine();

                switch (choice.ToLower())
                {
                    case "add frame":
                        {
                            var newNode = CreateNode(treeElement);
                            _tree.Tree[treeElement].Add(newNode);
                            _tree.Tree.Add(newNode, new List<InheritanceNodeModel>());

                            break;
                        }
                    case "add attribute":
                        {
                            var newFields = EnterAttributes();
                            foreach (var field in newFields)
                            {
                                treeElement.Fields.Add(field.Key, field.Value);
                            }
                            break;
                        }
                    case "set attribute":
                        {
                            UpdateAttribute(treeElement);
                            break;
                        }
                    case "show info":
                        {
                            PrintFrameInfo(treeElement);
                            break;
                        }
                    case "show all info":
                        {
                            PrintAllInfo(treeElement);
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

        // Have tired
        private void SetGlobalAttribute()
        {
            Console.Write("Enter name of the attribute: ");
            string atrbt = Console.ReadLine();
            if (!_tree.Tree.Select(item => item.Key.Fields.ContainsKey(atrbt)).Contains(true))
            {
                Console.WriteLine("\nThere is not such attributes");
                return;
            }

            Console.Write($"Enter value for {atrbt}:");
            string valueAtr = Console.ReadLine();

            _tree.Tree.Select(item =>
            {
                SetFrameAttribute(item.Key, atrbt, valueAtr);
                return item.Key;
            });
        }

        private void UpdateAttribute(InheritanceNodeModel node)
        {
            Console.Write("Enter name of the attribute: ");
            string atrbt = Console.ReadLine();
            if (node.Fields.ContainsKey(atrbt) && node.InheritaedField.ContainsKey(atrbt))
            {
                Console.WriteLine("\nThere is not such attributes");
                return;
            }

            Console.Write($"Enter value for {atrbt}:");
            string valueAtr = Console.ReadLine();
            SetFrameAttribute(node, atrbt, valueAtr);
        }

        private void SetFrameAttribute(InheritanceNodeModel model, string atrbt, string valueAtr)
        {
            try
            {
                model.Fields[atrbt] = valueAtr;
            }
            catch (KeyNotFoundException)
            {
                model.InheritaedField[atrbt] = valueAtr;
            }
        }

        private void PrintFrameInfo(InheritanceNodeModel node)
        {
            Console.WriteLine($"Frame {node.Name} has such attributes: ");
            foreach (var field in node.Fields)
            {
                Console.WriteLine($"{field.Key}: {field.Value};");
            }
        }

        private void PrintAllInfo(InheritanceNodeModel node)
        {
            PrintFrameInfo(node);
            Console.WriteLine($"Frame {node.Name} has such inherited attributes");
            foreach (var field in node.InheritaedField)
            {
                Console.WriteLine($"{field.Key}: {field.Value};");
            }

            Console.WriteLine($"Frame {node.Name} has such parents: ");
            foreach (string parent in node.Parents)
            {
                Console.WriteLine(parent);
            }
        }

        private InheritanceNodeModel CreateNode(InheritanceNodeModel parent = null)
        {
            Console.WriteLine("Enter new object name: ");
            string nodeName = Console.ReadLine();

            InheritanceNodeModel treeNode = new InheritanceNodeModel { Name = nodeName };

            treeNode.Parents = new List<string>();
            if (parent != null)
            {
                treeNode.Parents.AddRange(parent.Parents);
                treeNode.Parents.Add(parent.Name);
                treeNode.InheritaedField = parent.Fields;
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
