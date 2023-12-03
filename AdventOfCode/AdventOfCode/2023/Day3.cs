using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day3_2023
    {
        public int Execute()
        {
            var lines = File.ReadAllLines(@"../../../2023/Inputs/input3.txt");
            var foundSymbols = new List<Coordinate>();
            var engineNumbers = new List<EngineNumber>();
            var numberStrings = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            foreach (var line in lines.Select((value, i) => new { value, i }))
            {
                var numberString = string.Empty;
                var coordinates = new List<Coordinate>();
                foreach (var charVal in line.value.Select((value, i) => new { value, i }))
                {
                    if (numberStrings.Contains(charVal.value.ToString()))
                    {
						numberString += charVal.value;
                        coordinates.Add(new Coordinate() { X = charVal.i, Y = line.i });
					}
                    else
                    {
                        if(int.TryParse(numberString, out int val))
                        {
                            engineNumbers.Add(new EngineNumber() { Value = val, Coordinates = coordinates });
                            coordinates = new List<Coordinate>();
							numberString = string.Empty;
						}
                    }

					// Part 1
					//if (!numberStrings.Contains(charVal.value.ToString()) && charVal.value != '.')
					//{
					//    foundSymbols.Add(new Coordinate() { X = charVal.i, Y = line.i });
					//}

					// Part 2
					if (charVal.value == '*')
					{
						foundSymbols.Add(new Coordinate() { X = charVal.i, Y = line.i });
					}

				}

				if (int.TryParse(numberString, out int value))
				{
					engineNumbers.Add(new EngineNumber() { Value = value, Coordinates = coordinates });
					coordinates = new List<Coordinate>();
					numberString = string.Empty;
				}
			}

            // Part 1
            //var engineNumber2 = engineNumbers.Where(e => e.Coordinates.Where(c => foundSymbols.Any(f => (f.X - c.X >= -1 && f.X - c.X <= 1) && (f.Y - c.Y >= -1 && f.Y - c.Y <= 1))).Count() == 2);
            //var engineNumberValues = engineNumber2.Select(x => x.Value);

            // Part 2
            var sum = 0;
            foreach (var symbol in foundSymbols)
            {
				var en = engineNumbers.Where(e => e.Coordinates.Any(c => (c.X - symbol.X >= -1 && c.X - symbol.X <= 1) && (c.Y - symbol.Y >= -1 && c.Y - symbol.Y <= 1))).Select(x => x.Value);

                if (en.Count() == 2)
                {
                    sum += en.Aggregate((x, y) => x * y);
                }
			}

            // Part 1
            //return engineNumberValues.Sum();

            // Part 2
            return sum;
		}
    }

    public class EngineNumber
    {
        public int Value { get; set; }
		public List<Coordinate> Coordinates { get; set; }
    }

    public class Coordinate
    {
		public int X { get; set; }
        public int Y { get; set; }
    }
}