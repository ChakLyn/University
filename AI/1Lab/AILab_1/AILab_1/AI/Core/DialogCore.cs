using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AILab_1.AI.Models;
using AILab_1.Utils.Randomizer;
using AILab_1.Utils.OutputHelper;

namespace AILab_1.AI.Core
{
    public class DialogCore
    {
        private readonly DialogPhrases _phrases;
        private List<string> dynamicPhrasesList;
        private Dictionary<string, string> meaningDictionary;
        private AdditionalRandomizer additionalRandomizer;
        private Random random;

        public DialogCore(DialogPhrases phrases, Dictionary<int, double> dictionaryRandomTable)
        {
            _phrases = phrases;
            dynamicPhrasesList = new List<string>();
            meaningDictionary = new Dictionary<string, string>();
            random = new Random();
            additionalRandomizer = new AdditionalRandomizer(dictionaryRandomTable);
        }

        public void StartDialog()
        {
            bool dialogIsOn = true;

            Console.WriteLine("Бот обсуждений игр уже пришел, можете начинать общение.");

            while (dialogIsOn)
            {
                string userInput;
                Console.Write("Вы: ");
                userInput = Console.ReadLine();

                if (_phrases.GoodByPhrases.FirstOrDefault(phrase => userInput.ToLower().Contains(phrase.ToLower())) != null)
                {
                    Console.Write("Бот: ");
                    TypingHelper.TypeString(TakeRandomItem(_phrases.GoodByPhrases));
                    dialogIsOn = false;
                }
                else if (_phrases.GreetingsPhrases.FirstOrDefault(phrase => userInput.ToLower().Contains(phrase.ToLower())) != null)
                {
                    Console.Write("Бот: ");
                    TypingHelper.TypeString(TakeRandomItem(_phrases.GreetingsPhrases));
                }
                else
                {
                    MainThemeDiscussing(userInput);
                }
            }

            Console.WriteLine("\nБот ушел(\n");
            Console.ReadLine();
        }

        private void MainThemeDiscussing(string userInput)
        {
            string keyPhrase = _phrases.MainPhrases.Keys.FirstOrDefault(phrase => userInput.ToLower().Contains(phrase.ToLower()));
            string meaningPhrase = meaningDictionary.Keys.FirstOrDefault(phrase => userInput.ToLower().Contains(phrase.ToLower()));
            if (keyPhrase != null)
            {
                DiscussMainPhrase(keyPhrase);
            }
            else if (meaningPhrase != null)
            {
                BragOfKnowledge(meaningPhrase);
            }
            else
            {
                int choice = additionalRandomizer.DiscrDistr();
                if (dynamicPhrasesList.Count < 1)
                {
                    choice = random.Next(2, 3);
                }

                switch (choice)
                {
                    //Generate result from dynamicPhrasesList
                    case 1:
                        {
                            Console.Write("Бот: ");
                            TypingHelper.TypeString(TakeRandomItem(dynamicPhrasesList));
                            break;
                        }
                    //Ask meaning of the any word
                    case 2:
                        {
                            AskMeaningOfSomeWord(userInput);
                            break;
                        }
                    //Continue dialog
                    case 3:
                        {
                            Console.Write("Бот: ");
                            TypingHelper.TypeString(TakeRandomItem(_phrases.CountinuingPhrases));
                            break;
                        }

                }
            }
        }

        private void BragOfKnowledge(string meaningPhrase)
        {
            Console.Write("Бот: ");
            TypingHelper.TypeString($"Позволь похвастаться новыми знаниями) Я знаю, что {meaningPhrase} - {meaningDictionary[meaningPhrase]}");
        }

        private void AskMeaningOfSomeWord(string userInput)
        {
            List<string> wordsList = userInput.Split(' ').ToList();
            string askChoice = TakeRandomItem(wordsList);
            Console.Write("Бот: ");
            TypingHelper.TypeString($"Можешь рассказать по подробней о \"{askChoice}\"? Что это значит?");
            Console.Write("Вы: ");
            string meaningInput = Console.ReadLine();
            meaningDictionary.Add(askChoice, meaningInput);
            Console.Write("Бот: ");
            TypingHelper.TypeString("Спасибо, я это запомню). Что еще расскажешь?");
        }

        private void DiscussMainPhrase(string userInput)
        {
            Console.Write("Бот: ");
            TypingHelper.TypeString(_phrases.MainPhrases[userInput].ElementAt(0));
            dynamicPhrasesList.Add(_phrases.MainPhrases[userInput].ElementAt(1));
        }

        private string TakeRandomItem(IList<string> stringList)
        {
            return stringList.ElementAt(random.Next(0, stringList.Count));
        }
    }
}
