using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AILab_1.AI.Models
{
    public class InheritanceNodeModel
    {
        public List<string> Parents { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> Fields { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> InheritaedField { get; set; } = new Dictionary<string, string>();
        public List<InheritanceNodeModel> Childs { get; set; } = new List<InheritanceNodeModel>();
    }
}
