using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day1
    {
        public int Execute()
        {
            var lines = System.IO.File.ReadAllLines(@"../../../input.txt");

            var currentSum = 0;
            var groups = new int[250];
            var index = 0;

            foreach (var line in lines.Select((value, i) => new { i, value }))
            {
                if (line.value != "")
                {
                    currentSum += int.Parse(line.value);
                }
                else
                {
                    groups[index++] = currentSum;
                    currentSum = 0;
                }
            }

            groups = groups.OrderByDescending(x => x).ToArray();

            return groups[0] + groups[1] + groups[2];
        }
    }
}
