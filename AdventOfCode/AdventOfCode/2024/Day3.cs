using System.Text.RegularExpressions;

namespace AdventOfCode
{
	public class Day3_2024
	{
		public int Execute()
		{
			var lines = File.ReadAllLines(@"../../../2024/Inputs/day3.txt");

			return Part1(lines);
		}


		private int Part1(string[] lines)
		{
			string regex = @"mul\(\d{1,3},\d{1,3}\)";
			var total = 0;
			foreach (var line in lines)
			{
				var res = Regex.Matches(line, regex);
				foreach (Match match in res)
				{
					var mul = match.Groups[0].Value;
					var nums = mul.Replace("mul(", "")
						.Replace(")", "")
						.Split(",")
						.Select(x => int.Parse(x))
						.ToList();

					if (match.Captures.Count() > 1 || match.Groups.Count > 1)
					{
						var k = 0;
					}

					total += nums[0] * nums[1];
				}
			}
			// 41912275 - too low
			// 225858867 - too high
			// 196826776 - correct

			return total;
		}
	}
}
