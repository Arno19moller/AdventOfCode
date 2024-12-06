using System.Text.RegularExpressions;

namespace AdventOfCode
{
	public class Day4_2024
	{
		public int Execute()
		{
			var lines = File.ReadAllLines(@"../../../2024/Inputs/day4.txt");

			return Part2(lines);
		}

		private int Part2(string[] lines)
		{
			var xMasCount = 0;
			var yLimit = lines.Count() - 1;
			for (int i = 1; i < yLimit; i++) // loop through lines (2nd to 2nd last)
			{
				var aIndexes = FindIndexOfA(lines[i]); // find and loop through all indexes of 'A'
				foreach (var index in aIndexes)
				{
					if (index > 0 && index < (lines[i].Length - 1)) // not the char at very start or end
					{
						if (lines[i - 1][index - 1] == 'M' && lines[i - 1][index + 1] == 'S' &&
							lines[i + 1][index - 1] == 'M' && lines[i + 1][index + 1] == 'S')
						{
							xMasCount++;
						}
						else if (lines[i - 1][index - 1] == 'S' && lines[i - 1][index + 1] == 'S' &&
							lines[i + 1][index - 1] == 'M' && lines[i + 1][index + 1] == 'M')
						{
							xMasCount++;
						}
						else if (lines[i - 1][index - 1] == 'S' && lines[i - 1][index + 1] == 'M' &&
							lines[i + 1][index - 1] == 'S' && lines[i + 1][index + 1] == 'M')
						{
							xMasCount++;
						}
						else if (lines[i - 1][index - 1] == 'M' && lines[i - 1][index + 1] == 'M' &&
							lines[i + 1][index - 1] == 'S' && lines[i + 1][index + 1] == 'S')
						{
							xMasCount++;
						}
					}
				}
			}

			return xMasCount;
		}

		private List<int> FindIndexOfA(string line)
		{
			var indexes = new List<int>();
			foreach (var l in line.Select((Value, Index) => new { Value, Index }))
			{
				if (l.Value == 'A')
				{
					indexes.Add(l.Index);
				}
			}

			return indexes;
		}

		#region Part 1
		private int Part1(string[] lines)
		{
			var tally = 0;
			foreach (var line in lines)
			{
				tally += FindMatchCount(line);
			}

			tally += FindVertically(lines);
			tally += FindDiagonallyLeftToRight(lines);
			tally += FindDiagonallyRightToLeft(lines);

			// too high - 2539
			// correct - 2521

			return tally;
		}

		private int FindMatchCount(string line)
		{
			return Regex.Matches(line, "XMAS").Count +
			Regex.Matches(line, "SAMX").Count;
		}

		private int FindVertically(string[] lines)
		{
			var tally = 0;
			for (var i = 0; i < lines.Length; i++)
			{
				var line = new string(lines.Select(x => x[i]).ToArray());
				tally += FindMatchCount(line);
			}
			return tally;
		}

		private int FindDiagonallyLeftToRight(string[] lines)
		{
			var tally = 0;
			var xLimit = lines[0].Length;
			var yLimit = lines.Length;

			// Top left to bottom right
			foreach (var character in lines[0].Select((Value, Index) => new { Value, Index }))
			{
				// index 0,0 to 10,10
				var diagonalLine = new List<char>();
				for (var i = 0; i < yLimit; i++)
				{
					if (i < xLimit && (i + character.Index) < yLimit)
					{
						diagonalLine.Add(lines[i][i + character.Index]);
					}
				}

				tally += FindMatchCount(new string(diagonalLine.ToArray()));
			}
			foreach (var line in lines.Select((Value, Index) => new { Value, Index }))
			{
				// index 0,0 to 0,-10
				var diagonalLine = new List<char>();
				for (var i = 0; i < xLimit; i++)
				{
					if (i < xLimit && ((i + 1) + line.Index) < xLimit)
					{
						diagonalLine.Add(lines[(i + 1) + line.Index][i]);
					}
				}

				tally += FindMatchCount(new string(diagonalLine.ToArray()));
			}

			return tally;
		}

		private int FindDiagonallyRightToLeft(string[] lines)
		{
			var tally = 0;
			var xLimit = lines[0].Length;
			var yLimit = lines.Length;

			// Top left to bottom right
			foreach (var character in lines[0].Select((Value, Index) => new { Value, Index }))
			{
				// index 0,0 to 10,10
				var diagonalLine = new List<char>();
				for (var i = 0; i < yLimit; i++)
				{
					if (i < xLimit && (i + character.Index) < yLimit)
					{
						diagonalLine.Add(lines[(yLimit - 1) - i][i + character.Index]);
					}
				}

				tally += FindMatchCount(new string(diagonalLine.ToArray()));
			}
			foreach (var line in lines.Select((Value, Index) => new { Value, Index }))
			{
				// index 0,0 to 0,-10
				var diagonalLine = new List<char>();
				for (var i = 0; i < xLimit; i++)
				{
					if (i < xLimit && ((i + 1) + line.Index) < xLimit)
					{
						diagonalLine.Add(lines[(yLimit - 1) - ((i + 1) + line.Index)][i]);
					}
				}

				tally += FindMatchCount(new string(diagonalLine.ToArray()));
			}

			return tally;
		}
		#endregion
	}
}
