using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AILab_1.AI.Models
{
    public class DialogPhrases
    {
        public IList<string> GreetingsPhrases { get; set; }
        public IDictionary<string, IList<string>> MainPhrases { get; set; }
        public IList<string> CountinuingPhrases { get; set; }
        public IList<string> GoodByPhrases { get; set; }
    }
}
