using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AILab_1.AI.Models;

namespace AILab_1.AI.Models
{
    public class InheritanceTree
    {
        public Dictionary<InheritanceNodeModel, List<InheritanceNodeModel>> Tree { get; set; }
            = new Dictionary<InheritanceNodeModel, List<InheritanceNodeModel>>();
    }
}
