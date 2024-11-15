namespace AdventOfCode
{
	public class Day3_2021
	{
		public int Execute()
		{
			var lines = File.ReadAllLines(@"../../../2021/Inputs/day1.txt");

			var oxygen = 0;
			var scrubber = 0;

			var usedIndexes = new List<int>();
			var isOxygen = true;

			for (int k = 0; k < 2; k++)
			{
				for (int i = 0; i < lines.ElementAt(0).Length; i++)
				{
					var bits = new List<string>();

					foreach (var line in lines.Select((x, index) => new { value = x, index }))
					{
						if (usedIndexes.Count() == 0 || usedIndexes.Contains(line.index))
						{
							var bit = line.value.Substring(i, 1);
							bits.Add(bit);
						}
					}

					if (usedIndexes.Count() == 0 || usedIndexes.Count() > 1)
					{
						var grouped = bits.GroupBy(x => x).ToList();

						var zeros = grouped.First(x => x.Key == "0").Count();
						var ones = grouped.First(x => x.Key == "1").Count();

						var moreOfOxygenString = "";
						if (isOxygen)
						{
							moreOfOxygenString = zeros > ones ? "0" : "1";
						}
						else
						{
							moreOfOxygenString = zeros <= ones ? "0" : "1";
						}

						var indexes = lines.Select((x, index) => new { value = x, index })
						.Where(x => x.value.Substring(i, 1) == moreOfOxygenString && (usedIndexes.Count() == 0 || usedIndexes.Contains(x.index)))
						.Select(x => x.index);

						usedIndexes = indexes.ToList();
					}

					if (usedIndexes.Count() == 1)
					{
						var oxygenNum = lines.ElementAt(usedIndexes.First());
						usedIndexes.Clear();

						if (isOxygen)
						{
							oxygen = Convert.ToInt32(oxygenNum, 2);
						}
						else
						{
							scrubber = Convert.ToInt32(oxygenNum, 2);
						}

						isOxygen = false;
						break;
					}
				}
			}

			return oxygen * scrubber;
		}
	}
}
