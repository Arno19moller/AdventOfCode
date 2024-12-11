namespace AdventOfCode
{
	public class Day9_2024
	{
		public Int128 Execute()
		{
			var lines = File.ReadAllLines(@"../../../2024/Inputs/day9.txt");

			return Part2(lines);
		}

		private int Part2(string[] lines)
		{
			var fileSystem = new List<string>();
			var id = 0;
			foreach (var character in lines[0].Select((Value, Index) => new { Value, Index }))
			{
				for (var i = 0; i < int.Parse(character.Value.ToString()); i++)
				{
					if (character.Index % 2 == 0) // even
					{
						fileSystem.Add(id.ToString());
					}
					else // odd
					{
						fileSystem.Add(".");
					}
				}
				if (character.Index % 2 == 0) id++;
			}

			var emptyIndex = fileSystem.IndexOf(".");
			var lastBlockIndex = fileSystem.FindLastIndex(x => x != ".");
			while (emptyIndex < lastBlockIndex)
			{
				var firstBlockIndex = fileSystem.IndexOf(fileSystem[lastBlockIndex]);
				var blockLength = lastBlockIndex - firstBlockIndex + 1;
				var largestId = long.Parse(fileSystem[lastBlockIndex]);
				var startIndex = FirstConecutiveOfLength(fileSystem, blockLength);

				if (startIndex > -1 && startIndex < firstBlockIndex)
				{
					for (var i = 0; i < blockLength; i++)
					{
						var tmp = fileSystem[startIndex + i];
						fileSystem[startIndex + i] = fileSystem[firstBlockIndex + i];
						fileSystem[firstBlockIndex + i] = tmp;
					}
				}

				emptyIndex = fileSystem.IndexOf(".");
				lastBlockIndex = fileSystem.FindLastIndex(x => x != "." && long.Parse(x) < largestId);
			}

			var fileIds = fileSystem.Select(x => x != "." ? long.Parse(x) : 0).ToList();

			var checksum = fileIds.Select((Value, Index) => new { Value, Index })
				.Sum((x) => (x.Value * x.Index));


			return 0;
		}

		private int FirstConecutiveOfLength(List<string> fileSystem, int blockLength)
		{
			var emptyCount = 0;
			foreach (var file in fileSystem.Select((Value, Index) => new { Value, Index }))
			{
				if (file.Value == ".")
				{
					emptyCount++;
				}
				else
				{
					emptyCount = 0;
				}

				if (emptyCount >= blockLength)
				{
					return file.Index - emptyCount + 1;
				}
			}

			return -1;
		}

		private int Part1(string[] lines)
		{
			var fileSystem = new List<string>();
			var id = 0;
			foreach (var character in lines[0].Select((Value, Index) => new { Value, Index }))
			{
				for (var i = 0; i < int.Parse(character.Value.ToString()); i++)
				{
					if (character.Index % 2 == 0) // even
					{
						fileSystem.Add(id.ToString());
					}
					else // odd
					{
						fileSystem.Add(".");
					}
				}
				if (character.Index % 2 == 0) id++;
			}

			var emptyIndex = fileSystem.IndexOf(".");
			var blockIndex = fileSystem.FindLastIndex(x => x != ".");
			while (emptyIndex < blockIndex)
			{
				var tmp = fileSystem[emptyIndex];
				fileSystem[emptyIndex] = fileSystem[blockIndex];
				fileSystem[blockIndex] = tmp;

				emptyIndex = fileSystem.IndexOf(".");
				blockIndex = fileSystem.FindLastIndex(x => x != ".");
			}

			var fileIds = fileSystem.TakeWhile(x => x != ".").Select(x => long.Parse(x)).ToList();

			var checksum = fileIds.Select((Value, Index) => new { Value, Index })
				.Sum((x) => (x.Value * x.Index));


			return 0;
		}
	}
}