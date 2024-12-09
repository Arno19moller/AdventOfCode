namespace AdventOfCode
{
	public class Day7_2024
	{
		public Int128 Execute()
		{
			var lines = File.ReadAllLines(@"../../../2024/Inputs/day7.txt");

			return Part1(lines);
		}

		private Int128 Part1(string[] lines)
		{
			Int128 successCount = 0;
			foreach (var line in lines)
			{
				var splitLine = line.Split(':');
				var testingVal = Int128.Parse(splitLine[0]);
				var equations = splitLine[1].Split(" ")
								.Where(x => !x.Equals(""))
								.Select(x => int.Parse(x.Trim()));

				var c = new List<string> { "+", "*", "||" };
				var operators = GetCombinations(equations.Count() - 1, c);
				operators = operators.Where(x => x.Count() == equations.Count() - 1);

				foreach (var operatorCombi in operators)
				{
					Int128 value = 0;
					foreach (var equation in equations.Select((Value, Index) => new { Value, Index }))
					{
						if (equation.Index == 0)
						{
							value = equation.Value;
						}
						else if (operatorCombi[equation.Index - 1] == "*")
						{
							value *= equation.Value;
						}
						else if (operatorCombi[equation.Index - 1] == "+")
						{
							value += equation.Value;
						}
						else if (operatorCombi[equation.Index - 1] == "||")
						{
							value = Int128.Parse(value.ToString() + equation.Value.ToString());
						}
					}

					if (value == testingVal)
					{
						successCount += value;
						break;
					}
				}
			}
			return successCount;

			// Part 1:
			// 33150715 - too low
			// 33872033 - too low
			// 12918052603 - too low
			// 2654749936343 - correct

			// Part 2
			// 124060392153684 - correct
		}

		private static IEnumerable<string[]> GetCombinations(int maxLength, List<string> letters)
		{
			if (letters.Count() == 0)
				yield break;

			for (var agenda = new Queue<string[]>(new[] { Array.Empty<string>() });
					 agenda.Peek().Length < maxLength;)
			{
				var current = agenda.Dequeue();

				foreach (var letter in letters)
				{
					var next = current.Append(letter).ToArray();

					agenda.Enqueue(next);

					yield return next;
				}
			}
		}
	}
}
