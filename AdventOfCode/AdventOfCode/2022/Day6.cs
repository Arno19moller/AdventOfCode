using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day6
    {
        public int Execute()
        {
            var lines = System.IO.File.ReadAllLines(@"../../../input6.txt");
            var lineChar = lines[0].ToCharArray();
            var compareList = new List<char>();

            foreach (var line in lineChar.Select((value, i) => new { i, value }))
            {
                for (int i = 0; i < 14; i++)
                {
                    compareList.Add(lineChar[line.i + i]);
                }

                if(compareList.Select(x => x).Distinct().Count() == 14)
                {
                    return line.i + 14;
                }

                compareList.Clear();
            }
            return 0;
        }
    }
}
