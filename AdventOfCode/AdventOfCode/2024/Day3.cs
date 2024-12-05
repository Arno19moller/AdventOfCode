using System.Text.RegularExpressions;

namespace AdventOfCode
{
	public class Day3_2024
	{
		public int Execute()
		{
			var lines = File.ReadAllLines(@"../../../2024/Inputs/day3.txt");

			return Part2(lines);
		}


		private int Part2(string[] lines)
		{
			string regex = @"mul\(\d{1,3},\d{1,3}\)";
			var total = 0;
			var isLastDo = false;
			var isFirstLine = true;
			foreach (var line in lines)
			{
				var res = Regex.Matches(line, regex);
				foreach (Match match in res)
				{
					var mul = match.Groups[0].Value;

					var mulIndex = line.IndexOf(mul);
					var lineSubstring = line.Substring(0, mulIndex);
					var lastDoIndex = lineSubstring.LastIndexOf("do()");
					var lastDontIndex = lineSubstring.LastIndexOf("don't()");

					if ((isFirstLine && lastDoIndex == -1 && lastDontIndex == -1) ||    // first line and no 'do' or 'don't' encountered
						lastDoIndex > lastDontIndex ||                                  // 'do' found closer
						(isLastDo && lastDoIndex == -1 && lastDontIndex == -1))         // not first line, no 'do' or 'don't' found, but last found of prev line was 'do'
					{
						isLastDo = true;
						var nums = mul.Replace("mul(", "")
							.Replace(")", "")
							.Split(",")
							.Select(x => int.Parse(x))
							.ToList();

						total += nums[0] * nums[1];
					}
					else
					{
						isLastDo = false;
					}

				}
				isFirstLine = false;
			}

			// 114961848 - too high
			// 106780429 - correct

			return total;
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
