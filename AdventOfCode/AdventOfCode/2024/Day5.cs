namespace AdventOfCode
{
	public class Day5_2024
	{
		public int Execute()
		{
			var lines = File.ReadAllLines(@"../../../2024/Inputs/day5.txt");

			return Part2(lines);
		}

		private int Part2(string[] lines)
		{
			var rules = new List<Rule>();
			var middleNumSum = 0;

			foreach (var line in lines.TakeWhile(x => x.Contains("|")))
			{
				var nums = line.Split('|').Select(x => int.Parse(x));
				rules.Add(new Rule { First = nums.ElementAt(0), Second = nums.ElementAt(1) });
			}

			foreach (var line in lines.SkipWhile(x => x.Contains("|") || x == ""))
			{
				var nums = line.Split(',').Select(x => int.Parse(x)).ToList();
				var incorrect = Incorrect(rules, nums);

				while (Incorrect(rules, nums))
				{
					var rule = rules.First(r => nums.Contains(r.First) &&
														nums.Contains(r.Second) &&
														nums.IndexOf(r.First) > nums.IndexOf(r.Second));

					var firstIndex = nums.IndexOf(rule.First);
					var secondIndex = nums.IndexOf(rule.Second);
					nums[firstIndex] = rule.Second;
					nums[secondIndex] = rule.First;

				}

				if (incorrect) middleNumSum += nums.ElementAt(nums.Count() / 2);

			}
			return middleNumSum;
		}

		private bool Incorrect(List<Rule> rules, List<int> nums)
		{
			return rules.Exists(r => nums.Contains(r.First) &&
											nums.Contains(r.Second) &&
											nums.IndexOf(r.First) > nums.IndexOf(r.Second));
		}

		private int Part1(string[] lines)
		{
			var rules = new List<Rule>();
			var middleNumSum = 0;

			foreach (var line in lines.TakeWhile(x => x.Contains("|")))
			{
				var nums = line.Split('|').Select(x => int.Parse(x));
				rules.Add(new Rule { First = nums.ElementAt(0), Second = nums.ElementAt(1) });
			}

			foreach (var line in lines.SkipWhile(x => x.Contains("|") || x == ""))
			{
				var nums = line.Split(',').Select(x => int.Parse(x)).ToList();

				var matchingRules = rules.Where(r => nums.Contains(r.First) && nums.Contains(r.Second))
											 .All(r => nums.IndexOf(r.First) < nums.IndexOf(r.Second));

				if (matchingRules)
				{
					middleNumSum += nums.ElementAt(nums.Count() / 2);
				}
			}
			return middleNumSum;
		}

		private struct Rule
		{
			public int First;
			public int Second;
		}
	}
}
