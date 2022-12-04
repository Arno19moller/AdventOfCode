using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day2
    {
        public int Execute()
        {
            var lines = System.IO.File.ReadAllLines(@"../../../input2.txt");
            var total = 0;

            foreach (var line in lines.Select((value, i) => new { i, value }))
            {
                var opponent = line.value.Substring(0, 1);
                var me = line.value.Substring(2);

                var opponentInt = GetOpponentInt(opponent);
                var meInt = GetMeInt(me);
                var round = 0;

                if (me == "X") // loose
                {
                    if (opponent == "A") // Rock
                    {
                        round += 3;
                    }
                    if (opponent == "B") // Paper
                    {
                        round += 1;
                    }
                    if (opponent == "C") // Scissor
                    {
                        round += 2;
                    }
                }

                if (me == "Y") // draw
                {
                    round += 3;
                    if (opponent == "A") // Rock
                    {
                        round += 1;
                    }
                    if (opponent == "B") // Paper
                    {
                        round += 2;
                    }
                    if (opponent == "C") // Scissor
                    {
                        round += 3;
                    }
                }

                if (me == "Z") // win
                {
                    round += 6;
                    if (opponent == "A") // Rock
                    {
                        round += 2;
                    }
                    if (opponent == "B") // Paper
                    {
                        round += 3;
                    }
                    if (opponent == "C") // Scissor
                    {
                        round += 1;
                    }
                }

                total += round;
            }

            return total;
        }

        private int GetOpponentInt(String opponent)
        {
            if (opponent == "A")
            {
                return 1;
            }
            else if (opponent == "B")
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        private int GetMeInt(String me)
        {
            if (me == " X")
            {
                return 1;
            }
            else if (me == " Y")
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
    }
}
