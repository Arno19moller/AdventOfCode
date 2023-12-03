using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day2_2023
    {
        public int Execute()
        {
            var lines = File.ReadAllLines(@"../../../2023/Inputs/input2.txt");
            var rowCount = lines.Length;
            var gamesTotal = 0;


            foreach (var line in lines.Select((value, i) => new { value, i }))
            {
                // Part 1
                //var game = line.value.Substring(0, line.value.IndexOf(":"));
                //var gameNrString = game.Split(" ")[1];
                //var gameNr = int.Parse(gameNrString);
                //var canBePlayed = true;

                // Part 2
                var cubesDict = new Dictionary<string, int>
                {
                    { "red", 0 },
                    { "green", 0 },
                    { "blue", 0 }
                };

                var sets = line.value.Substring(line.value.IndexOf(":") + 1).Split(";");
                foreach (var set in sets)
                {
                    var cubes = set.Split(",");

                    foreach (var c in cubes)
                    {
                        // Part 2
                        var cubeColor = c.Split(" ")[2];
                        var cubeNum = int.Parse(c.Split(" ")[1].ToString());

                        var cubeEnum = (Cubes)Enum.Parse(typeof(Cubes), cubeColor);
                        var cubeDictVal = cubesDict[cubeEnum.ToString()];

                        if (cubeNum > cubeDictVal)
                        {
                            cubesDict[cubeEnum.ToString()] = cubeNum;
                        }


                        // Part 1
                        //var cubeEnum = (Cubes)Enum.Parse(typeof(Cubes), cubeColor);
                        //if (cubeNum > (int)cubeEnum)
                        //{
                        //    canBePlayed = false;
                        //    break;
                        //}
                        //var i = 0;
                    }
                }
                // Part 2
                var gamePower = cubesDict.Values.ToList().Aggregate((x, y) => x * y);
                gamesTotal += gamePower;

				// Part 1
				//if (canBePlayed)
				//{
				//    gamesTotal += gameNr;
				//}
			}

            return gamesTotal;
        }
    }

    public enum Cubes
    {
        red = 12,
        green = 13,
        blue = 14,
    }
}