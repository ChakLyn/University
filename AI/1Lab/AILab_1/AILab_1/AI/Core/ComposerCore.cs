using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AILab_1.AI.Models;
using AILab_1.AI.Models.Enums;

namespace AILab_1.AI.Core
{
    public class ComposerCore
    {
        private readonly Parts _parts;
        private List<List<((string partName, int partCost), int routeLevel)>> _routes;
        private Random random = new Random();
        private string ComposerFormat = "\nПуть: \n{0}\nОбщая стоимость: {1}";

        public ComposerCore(Parts parts)
        {
            _parts = parts;
        }

        public void StartCompose()
        {
            bool exitFlag = true;
            Console.WriteLine("Composer has started right now! You can work.\n");

            Console.WriteLine("There are such elements:");
            var presentationCollection = _parts.PartDictionary.Where(item => item.Value.VarianList.Count > 0);

            foreach (var part in presentationCollection)
            {
                Console.WriteLine(part.Key);
            }

            while (exitFlag)
            {

                Console.Write("\nEnter the command: ");
                string choice = Console.ReadLine();
                switch (choice.ToLower())
                {
                    case "rand":
                        {
                            var randRoute = BuildVariantWay(presentationCollection.FirstOrDefault().Key, SelectRouteEnum.Rand);
                            Console.WriteLine(ComposerFormat, randRoute.Path, randRoute.Cost);
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
                            Console.WriteLine("Rand command - build rand combiantion by rout of parts.");
                            Console.WriteLine("Max - build rout by max cost ");
                            Console.WriteLine("Min - build rout by min cost");
                            Console.WriteLine("Exit - exit program");
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

        private (string Path, int Cost) CostDivider(List<(string Path, int Cost)> pathsList, SelectRouteEnum costVariant)
        {
            var sortedPaths = pathsList.OrderBy(item => item.Cost);
            switch (costVariant)
            {
                case SelectRouteEnum.ByMin:
                    {
                        return sortedPaths.FirstOrDefault();
                    }
                case SelectRouteEnum.ByMax:
                    {
                        return sortedPaths.LastOrDefault();
                    }
                case SelectRouteEnum.Rand:
                    {
                        return sortedPaths.ElementAtOrDefault(random.Next(0, sortedPaths.Count()));
                    }
            }
            return ("", 0);
        }

        private (string Path, int Cost) BuildVariantWay(string partName, SelectRouteEnum costEnum, int depthLevel = 0)
        {
            StringBuilder pathBuilder = new StringBuilder();
            pathBuilder.Append('\t', depthLevel);
            int buffCost = 0;

            List<(string Path, int Cost)> tempList = new List<(string Path, int Cost)>();

            try
            {
                PartModel partModel = _parts.PartDictionary[partName];
                pathBuilder.AppendLine($"-> {partName}. Стоимость: {partModel.Cost}");
                buffCost = partModel.Cost;
                if (partModel.VarianList.Count > 0)
                {
                    if (!partModel.AndFlag)
                    {
                        foreach (string variantPartName in partModel.VarianList)
                        {
                            tempList.Add(BuildVariantWay(variantPartName, costEnum, depthLevel + 1));
                        }
                        var tempPath = CostDivider(tempList, costEnum);
                        pathBuilder.Append(tempPath.Path);
                        buffCost += tempPath.Cost;
                    }
                    else
                    {
                        foreach (string variantPartName in partModel.VarianList)
                        {
                            var tempPath = BuildVariantWay(variantPartName, costEnum, depthLevel + 1);
                            pathBuilder.Append(tempPath.Path);
                            buffCost += tempPath.Cost;
                        }
                    }
                }

            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Error.{partName} Not found!");
            }
            return (pathBuilder.ToString(), buffCost);
        }
    }
}
