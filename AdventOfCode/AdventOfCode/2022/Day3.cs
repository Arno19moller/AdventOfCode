using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day3
    {
        public int Execute()
        {

            var lines = System.IO.File.ReadAllLines(@"../../../2022/Inputs/input3.txt");
            var total = 0;
            var roundDone = false;
            var emptyString = " ";
            var emptyChar = emptyString[0];

            for (int i = 0; i < lines.Length; i += 3)
            {
                var line = lines[i].ToArray();

                while (true)
                {
                    var marker = lines[i + 1].ToArray().First(x => line.Any(y => y == x));

                    if (lines[i + 2].ToArray().Any(x => marker == x))
                    {
                        var groupMarker = lines[i + 2].ToArray().First(x => marker == x);

                        var ascii = Encoding.ASCII.GetBytes(groupMarker.ToString());
                        if (char.IsUpper(groupMarker))
                        {
                            ascii[0] -= 38;
                        }
                        else
                        {
                            ascii[0] -= 96;
                        }
                        total += ascii[0];

                        break;
                    }
                    else
                    {
                        //var index = line.ToString().IndexOf(marker);
                        //line = line.Skip(index > 0 ? index : 2).ToString().ToCharArray();
                        var index = lines[i + 1].IndexOf(marker);
                        lines[i + 1] = lines[i + 1].Substring(index+1);
                    }
                }
            }

            return total;
        }

        public int part1()
        {
            var lines = System.IO.File.ReadAllLines(@"../../../input3.txt");
            var total = 0;
            var roundDone = false;

            foreach (var line in lines)
            {
                var half1 = line.Substring(0, line.Length / 2);
                var half2 = line.Substring(line.Length / 2);

                var duplicate = "";
                foreach (var character in half1.Select((value, i) => new
                {
                    i,
                    value
                }))
                {
                    if (!roundDone && half2.ToArray().Any(x => x == character.value))
                    {
                        var ascii = Encoding.ASCII.GetBytes(character.value.ToString());
                        if (char.IsUpper(character.value))
                        {
                            ascii[0] -= 38;
                        }
                        else
                        {
                            ascii[0] -= 96;
                        }
                        total += ascii[0];

                        break;
                        //roundDone = true;
                    }
                }
            }

            return total;
        }
    }
}
