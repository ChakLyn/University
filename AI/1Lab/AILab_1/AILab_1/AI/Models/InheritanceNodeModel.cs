using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AILab_1.AI.Models
{
    public class InheritanceNodeModel
    {
        public string Parent { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> Fields { get; set; } = new Dictionary<string, string>();        
    }
}
