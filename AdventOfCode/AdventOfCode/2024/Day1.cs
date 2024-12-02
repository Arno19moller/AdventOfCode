namespace AdventOfCode
{
	public class Day1_2024
	{
		public int Execute()
		{
			var lines = File.ReadAllLines(@"../../../2024/Inputs/day1.txt");

			return Part2(lines);
		}

		private int Part2(string[] lines)
		{
			var list1 = new List<int>();
			var list2 = new List<int>();
			var distance = new List<int>();

			foreach (var line in lines)
			{
				var numberStrings = line.Split("   ");
				list1.Add(int.Parse(numberStrings[0]));
				list2.Add(int.Parse(numberStrings[1]));
			}

			foreach (var num in list1)
			{
				var count = list2.Where(x => x == num).Count();
				distance.Add(num * count);
			}

			return distance.Sum();
		}

		private int Part1(string[] lines)
		{
			var list1 = new List<KeyValuePair<int, bool>>();
			var list2 = new List<KeyValuePair<int, bool>>();
			var distance = new List<int>();

			foreach (var line in lines)
			{
				var numberStrings = line.Split("   ");
				list1.Add(new KeyValuePair<int, bool>(int.Parse(numberStrings[0]), false));
				list2.Add(new KeyValuePair<int, bool>(int.Parse(numberStrings[1]), false));
			}

			while (list1.Exists(x => !x.Value))
			{
				var smallest1 = FindMin(list1);
				list1[smallest1] = new KeyValuePair<int, bool>(list1[smallest1].Key, true);

				var smallest2 = FindMin(list2);
				list2[smallest2] = new KeyValuePair<int, bool>(list2[smallest2].Key, true);

				distance.Add(Math.Abs(list1[smallest1].Key - list2[smallest2].Key));
			}

			return distance.Sum();
		}

		private int FindMin(List<KeyValuePair<int, bool>> list)
		{
			var index = -1;
			var minVal = 999999;
			foreach (var pair in list.Select((x, i) => new { val = x, index = i }))
			{
				if (!pair.val.Value && pair.val.Key < minVal)
				{
					minVal = pair.val.Key;
					index = pair.index;
				}
			}

			return index;
		}
	}
}
