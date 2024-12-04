namespace AdventOfCode
{
	public class Day2_2024
	{
		public int Execute()
		{
			var lines = File.ReadAllLines(@"../../../2024/Inputs/day2.txt");

			return Part2(lines);
		}


		private int Part1(string[] lines)
		{
			var safeCount = 0;
			foreach (var line in lines)
			{
				var levels = line.Split(" ").Select(x => int.Parse(x));

				var prev = -1;
				var increasing = -1;
				var safe = true;
				foreach (var level in levels)
				{
					if (increasing == -1 && prev != -1)
					{
						increasing = level > prev ? 1 : 0;
					}

					if (prev != -1 &&
						((level > prev && increasing == 0) ||
						(level < prev && increasing == 1) ||
						(level == prev) ||
						(Math.Abs(level - prev) > 3)))
					{
						safe = false; break;

					}

					prev = level;
				}

				safeCount += safe ? 1 : 0;
			}

			return safeCount;
		}

		private int Part2(string[] lines)
		{
			var safeCount = 0;

			foreach (var line in lines)
			{
				var levels = line.Split(" ").Select(x => int.Parse(x)).ToList();

				var index = Part2Looper(levels);
				if (index > -1)
				{
					var anySafe = false;
					foreach (var level in levels.Select((x, i) => new { index = i, value = x }))
					{
						var lst = new List<int>();
						lst.AddRange(levels);
						lst.RemoveAt(level.index);

						index = Part2Looper(lst);
						if (index == -1)
						{
							anySafe = true;
						}
					}

					if (!anySafe)
					{
						safeCount--;
					}
				}
				safeCount++;
			}

			return safeCount;
		}

		private int Part2Looper(List<int> levels)
		{
			try
			{
				var prev = -1;
				var increasing = -1;
				var index = 0;
				foreach (var level in levels)
				{
					if (increasing == -1 && prev != -1)
					{
						increasing = level > prev ? 1 : 0;
					}

					if (prev != -1 &&
						((level > prev && increasing == 0) ||
						(level < prev && increasing == 1) ||
						(level == prev) ||
						(Math.Abs(level - prev) > 3)))
					{
						return index;
					}

					prev = level;
					index++;
				}

				return -1;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
