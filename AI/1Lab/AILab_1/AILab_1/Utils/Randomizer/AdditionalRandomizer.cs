using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AILab_1.Utils.Randomizer
{
    public class AdditionalRandomizer
    {
        private Random nParametr;
        private readonly Dictionary<int, double> _dictionaryRandomTable;

        public AdditionalRandomizer(Dictionary<int, double> dictionaryRandomTable)
        {
            nParametr = new Random();
            _dictionaryRandomTable = dictionaryRandomTable;
        }

        public int DiscrDistr()
        {
            List<double> sum = new List<double>();
            double tempN = nParametr.NextDouble();

            for (int i = 0; i < _dictionaryRandomTable.Count; i++)
            {
                if (i == 0)
                    sum.Add(_dictionaryRandomTable.Values.ElementAt(i));
                else
                    sum.Add(sum[i - 1] + _dictionaryRandomTable.Values.ElementAt(i));
            }
            int index = 0;

            for (int j = 0; j < sum.Count; j++)
            {
                if (j == 0 && tempN <= sum[j])
                {
                    index = j;
                    break;
                }
                if (j > 0 && (sum[j - 1] < tempN && tempN <= sum[j]))
                {
                    index = j;
                    break;
                }
            }
            return _dictionaryRandomTable.Keys.ElementAt(index);
        }
    }
}
