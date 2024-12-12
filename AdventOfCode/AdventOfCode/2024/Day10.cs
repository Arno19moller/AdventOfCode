namespace AdventOfCode
{
	public class Day10_2024
	{
		public Int128 Execute()
		{
			var lines = File.ReadAllLines(@"../../../2024/Inputs/day10.txt");

			return Part1(lines);
		}

		private int Part1(string[] lines)
		{
			var xLimit = lines[0].Length;
			var yLimit = lines.Length;

			var trails = new int[yLimit, xLimit];
			var startingPoints = new List<Position>();

			foreach (var line in lines.Select((Value, Index) => new { Value, Index }))
			{
				for (var i = 0; i < line.Value.Length; i++)
				{
					trails[line.Index, i] = int.Parse(line.Value[i].ToString());
					if (trails[line.Index, i] == 0)
					{
						startingPoints.Add(new Position { Y = line.Index, X = i });
					}
				}
			}

			var count = 0;
			foreach (var start in startingPoints)
			{
				var k = TraverseTrail2(trails, start, 0, xLimit, yLimit, new List<Position>());
				count += k;
			}
			// 1249 - too high
			return count;
		}

		private int TraverseTrail2(int[,] trail, Position currPosition, int currValue, int xLimit, int yLimit, List<Position> nines, int trailCount = 0)
		{
			if (currValue == 9)// && !nines.Exists(x => x.X == currPosition.X && x.Y == currPosition.Y))
			{
				nines.Add(currPosition);
				trailCount++;
				return trailCount;
			}
			else
			{
				// Up
				if (currPosition.Y - 1 >= 0 && (trail[currPosition.Y - 1, currPosition.X] == currValue + 1))
				{
					trailCount = TraverseTrail2(trail, new Position { Y = currPosition.Y - 1, X = currPosition.X }, currValue + 1, xLimit, yLimit, nines, trailCount);
				}
				// Right
				if (currPosition.X + 1 < xLimit && (trail[currPosition.Y, currPosition.X + 1] == currValue + 1))
				{
					trailCount = TraverseTrail2(trail, new Position { Y = currPosition.Y, X = currPosition.X + 1 }, currValue + 1, xLimit, yLimit, nines, trailCount);
				}
				// Down
				if (currPosition.Y + 1 < yLimit && (trail[currPosition.Y + 1, currPosition.X] == currValue + 1))
				{
					trailCount = TraverseTrail2(trail, new Position { Y = currPosition.Y + 1, X = currPosition.X }, currValue + 1, xLimit, yLimit, nines, trailCount);
				}
				// Left
				if (currPosition.X - 1 >= 0 && (trail[currPosition.Y, currPosition.X - 1] == currValue + 1))
				{
					trailCount = TraverseTrail2(trail, new Position { Y = currPosition.Y, X = currPosition.X - 1 }, currValue + 1, xLimit, yLimit, nines, trailCount);
				}
			}
			return trailCount;
		}

		private int TraverseTrail1(int[,] trail, Position currPosition, int currValue, int xLimit, int yLimit, List<Position> nines)
		{
			if (currValue == 9 && !nines.Exists(x => x.X == currPosition.X && x.Y == currPosition.Y))
			{
				nines.Add(currPosition);
				return nines.Count;
			}
			else
			{
				// Up
				if (currPosition.Y - 1 >= 0 && (trail[currPosition.Y - 1, currPosition.X] == currValue + 1))
				{
					TraverseTrail1(trail, new Position { Y = currPosition.Y - 1, X = currPosition.X }, currValue + 1, xLimit, yLimit, nines);
				}
				// Right
				if (currPosition.X + 1 < xLimit && (trail[currPosition.Y, currPosition.X + 1] == currValue + 1))
				{
					TraverseTrail1(trail, new Position { Y = currPosition.Y, X = currPosition.X + 1 }, currValue + 1, xLimit, yLimit, nines);
				}
				// Down
				if (currPosition.Y + 1 < yLimit && (trail[currPosition.Y + 1, currPosition.X] == currValue + 1))
				{
					TraverseTrail1(trail, new Position { Y = currPosition.Y + 1, X = currPosition.X }, currValue + 1, xLimit, yLimit, nines);
				}
				// Left
				if (currPosition.X - 1 >= 0 && (trail[currPosition.Y, currPosition.X - 1] == currValue + 1))
				{
					TraverseTrail1(trail, new Position { Y = currPosition.Y, X = currPosition.X - 1 }, currValue + 1, xLimit, yLimit, nines);
				}
			}
			return nines.Count;
		}

		private class Position
		{
			public int X;
			public int Y;
		}
	}
}