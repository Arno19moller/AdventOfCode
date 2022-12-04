using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day4
    {
        public int Execute()
        {
            var lines = System.IO.File.ReadAllLines(@"../../../input4.txt");
            //List<int>[] a = new List<int>[2000];
            var total = 0;

            foreach (var line in lines)
            {
                var elf1 = line.Substring(0, line.IndexOf(","));
                var elf2 = line.Substring(line.IndexOf(",") + 1);

                var start1 = int.Parse(elf1.Substring(0, elf1.IndexOf("-")));
                var end1 = int.Parse(elf1.Substring(elf1.IndexOf("-") + 1));
                
                var start2 = int.Parse(elf2.Substring(0, elf2.IndexOf("-")));
                var end2 = int.Parse(elf2.Substring(elf2.IndexOf("-") + 1));

                var elf1List = new List<int>();
                var elf2List = new List<int>();

                for (var i = start1; i < end1 + 1; i++)
                {
                    elf1List.Add(i);
                }

                for (var k = start2; k < end2 + 1; k++)
                {
                    elf2List.Add(k);
                }
                //bool isSubset1 = !elf1List.Except(elf2List).Any();
                //bool isSubset2 = !elf2List.Except(elf1List).Any();

                var isOverlapping = elf1List.Intersect(elf2List).Any();

                //total += isSubset1 || isSubset2 ? 1 : 0;
                total += isOverlapping ? 1 : 0;
            }


            return total;
        }
    }
}
