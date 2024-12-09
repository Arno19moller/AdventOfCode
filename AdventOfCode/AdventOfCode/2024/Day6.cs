namespace AdventOfCode
{
	public class Day6_2024
	{
		List<MapCoordinate> coordinates = new List<MapCoordinate>();

		public int Execute()
		{
			var lines = File.ReadAllLines(@"../../../2024/Inputs/day6.txt");

			return Part1(lines);
		}

		private int Part2(string[] lines)
		{
			var guardPos = new MapCoordinate();

			coordinates = PlotMap(lines, ref guardPos);

			var encounteredObstacles = new List<MapCoordinate>();
			var direction = Direction.Up;
			var xLimit = lines[0].Length;
			var yLimit = lines.Length;

			while (guardPos.X >= 0 && guardPos.X < xLimit &&
					guardPos.Y >= 0 && guardPos.Y < yLimit)
			{
				switch (direction)
				{
					case Direction.Up:
						if (CanMoveGuard(guardPos.X, guardPos.Y - 1, coordinates, ref direction))
						{
							guardPos.Y--;
						}
						else
						{
							encounteredObstacles.Add(new MapCoordinate { X = guardPos.X, Y = guardPos.Y - 1 });
						}
						break;
					case Direction.Right:
						if (CanMoveGuard(guardPos.X + 1, guardPos.Y, coordinates, ref direction))
						{
							guardPos.X++;
						}
						else
						{
							encounteredObstacles.Add(new MapCoordinate { X = guardPos.X + 1, Y = guardPos.Y });
						}
						break;
					case Direction.Down:
						if (CanMoveGuard(guardPos.X, guardPos.Y + 1, coordinates, ref direction))
						{
							guardPos.Y++;
						}
						else
						{
							encounteredObstacles.Add(new MapCoordinate { X = guardPos.X, Y = guardPos.Y + 1 });
						}
						break;
					case Direction.Left:
						if (CanMoveGuard(guardPos.X - 1, guardPos.Y, coordinates, ref direction))
						{
							guardPos.X--;
						}
						else
						{
							encounteredObstacles.Add(new MapCoordinate { X = guardPos.X - 1, Y = guardPos.Y });
						}
						break;
				}

				if (encounteredObstacles.Count() == 3)
				{

				}

			}

			return 0;
		}

		private int Part1(string[] lines)
		{

			var guardPos = new MapCoordinate();

			coordinates = PlotMap(lines, ref guardPos);

			var direction = Direction.Up;
			var xLimit = lines[0].Length;
			var yLimit = lines.Length;
			while (guardPos.X >= 0 && guardPos.X < xLimit &&
					guardPos.Y >= 0 && guardPos.Y < yLimit)
			{
				switch (direction)
				{
					case Direction.Up:
						if (CanMoveGuard(guardPos.X, guardPos.Y - 1, coordinates, ref direction))
						{
							guardPos.Y--;
						}
						break;
					case Direction.Right:
						if (CanMoveGuard(guardPos.X + 1, guardPos.Y, coordinates, ref direction))
						{
							guardPos.X++;
						}
						break;
					case Direction.Down:
						if (CanMoveGuard(guardPos.X, guardPos.Y + 1, coordinates, ref direction))
						{
							guardPos.Y++;
						}
						break;
					case Direction.Left:
						if (CanMoveGuard(guardPos.X - 1, guardPos.Y, coordinates, ref direction))
						{
							guardPos.X--;
						}
						break;
				}
			}

			return coordinates.Where(x => x.Visited).Count();
		}

		private List<MapCoordinate> PlotMap(string[] lines, ref MapCoordinate guardPos)
		{
			foreach (var line in lines.Select((Value, Index) => new { Value, Index }))
			{
				foreach (var pos in line.Value.Select((Value, Index) => new { Value, Index }))
				{
					if (pos.Value != '^')
					{
						coordinates.Add(new MapCoordinate { X = pos.Index, Y = line.Index, IsObstacle = pos.Value == '#', Visited = false });
					}
					else
					{
						guardPos.X = pos.Index;
						guardPos.Y = line.Index;
						coordinates.Add(new MapCoordinate { X = pos.Index, Y = line.Index, IsObstacle = false, Visited = true });
					}
				}
			}

			return coordinates;
		}

		private bool CanMoveGuard(int X, int Y, List<MapCoordinate> coordinates, ref Direction direction)
		{
			try
			{
				var coordinate = coordinates.First(x => x.X == X && x.Y == Y);
				var position = coordinates.IndexOf(coordinate);
				if (coordinates[position].IsObstacle)
				{
					var directionInt = ((int)direction + 1) % 4;
					direction = (Direction)directionInt;
					return false;
				}
				else
				{
					coordinates[position] = new MapCoordinate
					{
						X = X,
						Y = Y,
						IsObstacle = false,
						Visited = true
					};
					return true;
				}
			}
			catch (Exception)
			{
				return true;
			}
		}

		private struct MapCoordinate
		{
			public int X;
			public int Y;
			public bool IsObstacle;
			public bool Visited;
		}

		private enum Direction
		{
			Up = 0,
			Right = 1,
			Down = 2,
			Left = 3
		}
	}
}
