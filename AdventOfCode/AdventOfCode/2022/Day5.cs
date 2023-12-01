using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day5
    {
        public string Execute()
        {
            var lines = System.IO.File.ReadAllLines(@"../../../input5.txt");
            Stack<string>[] stacksReverse = new Stack<string>[9];
            Stack<string>[] stacks= new Stack<string>[9];
            var stackDoneRead = false;
            var index = 1;

            for (var k = 0; k < 9; k++)
            {
                stacksReverse[k] = new Stack<string>();
                stacks[k] = new Stack<string>();
            }

            foreach (var line in lines)
            {
                var stack = line.ToCharArray();

                if (line == "")
                {
                    stackDoneRead = true;
                    for (var i = 0; i < 9; i++)
                    {
                        var count = stacksReverse[i].Count();
                        for (var t = 0; t < count; t++)
                        {
                            var toMove = stacksReverse[i].Pop();
                            stacks[i].Push(toMove);
                        }
                    }
                    continue;
                }

                if (!stackDoneRead)
                {
                    for (var i = 0; i < 9; i++)
                    {
                        var letter = stack[index];

                        if (char.IsUpper(letter))
                        {
                            stacksReverse[i].Push(letter.ToString());
                        }
                        index += 4;
                    }
                    index = 1;
                }

                if (stackDoneRead)
                {
                    var input = line.Split(" ");
                    var numberToMove = int.Parse(input[1]);
                    var fromStack = int.Parse(input[3]);
                    var toStack = int.Parse(input[5]);

                    fromStack -= 1;
                    toStack -= 1;
                    var stackPiece = "";
                    for (int j = 0; j < numberToMove; j++)
                    {
                        if (stacks[fromStack].Count() > 0)
                        {
                            stackPiece += stacks[fromStack].Peek();
                            stacks[fromStack].Pop();
                        }
                    }

                    numberToMove -= 1;
                    for (int q = numberToMove; q >= 0; q--)
                    {
                        stacks[toStack].Push(stackPiece[q].ToString());
                    }
                }
            }

            var resultString = "";
            for (var k = 0; k < 9; k++)
            {
                if (stacks[k].Count() > 0)
                {
                    resultString += stacks[k].Pop();
                }
                else
                {
                    resultString += " ";
                }
            }

            return resultString;
        }
    }
}
