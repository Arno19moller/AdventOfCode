namespace AdventOfCode
{
	public class Day8_2024
	{
		public Int128 Execute()
		{
			var lines = File.ReadAllLines(@"../../../2024/Inputs/day8.txt");

			return Part1(lines);
		}

		private int Part1(string[] lines)
		{
			var antennas = new List<Antenna>();
			foreach (var line in lines.Select((Value, Index) => new { Value, Index }))
			{
				foreach (var character in line.Value.Select((Value, Index) => new { Value, Index }))
				{
					if (character.Value != '.')
					{
						antennas.Add(new Antenna
						{
							Type = character.Value,
							X = character.Index,
							Y = line.Index
						});
					}
				}
			}

			var antinodes = new List<Antenna>();

			foreach (var ant in antennas)
			{
				var anti = GetAntinodes(ant, antennas, lines[0].Count(), lines.Count());
				antinodes.AddRange(anti);
			}

			var antinodeCount = antinodes.GroupBy(ant => new { ant.X, ant.Y })
				.Select(y => y.First())
				.Count();

			return antinodeCount;
		}

		private List<Antenna> GetAntinodes(Antenna antenna, List<Antenna> otherAntennas, int xLimit, int yLimit)
		{
			var antinodes = new List<Antenna>();
			otherAntennas = otherAntennas.Where(ant => ant.Type == antenna.Type).ToList();

			foreach (var ant in otherAntennas)
			{
				if (ant.X != antenna.X || ant.Y != antenna.Y)
				{
					var xDistance = Math.Abs(ant.X - antenna.X);
					var yDistance = Math.Abs(ant.Y - antenna.Y);

					var top = ant.Y < antenna.Y ? ant : antenna;
					var bottom = ant.Y > antenna.Y ? ant : antenna;

					if (top.X < bottom.X) // top left to bottom right
					{
						var count = 0;
						while (true)
						{
							var x = top.X - (xDistance * count);
							var y = top.Y - (yDistance * count);
							if (!CheckAndAddAntinode(antinodes, x, y, xLimit, yLimit))
							{
								break;
							}
							count++;
						}

						count = 0;
						while (true)
						{
							var x = bottom.X + (xDistance * count);
							var y = bottom.Y + (yDistance * count);
							if (!CheckAndAddAntinode(antinodes, x, y, xLimit, yLimit))
							{
								break;
							}
							count++;
						}
					}
					else // top right to bottom left
					{
						var count = 0;
						while (true)
						{
							var x = top.X + (xDistance * count);
							var y = top.Y - (yDistance * count);
							if (!CheckAndAddAntinode(antinodes, x, y, xLimit, yLimit))
							{
								break;
							}
							count++;
						}

						count = 0;
						while (true)
						{
							var x = bottom.X - (xDistance * count);
							var y = bottom.Y + (yDistance * count);
							if (!CheckAndAddAntinode(antinodes, x, y, xLimit, yLimit))
							{
								break;
							}
							count++;
						}
					}
				}
			}
			return antinodes;
		}

		private bool CheckAndAddAntinode(List<Antenna> antinodesCount, int x, int y, int xLimit, int yLimit)
		{
			if (x >= 0 && x < xLimit && y >= 0 && y < yLimit)
			{
				antinodesCount.Add(new Antenna
				{
					Type = '#',
					X = x,
					Y = y
				});

				return true;
			}
			else
			{
				return false;
			}
		}

		private class Antenna
		{
			public char Type { get; set; }
			public int X { get; set; }
			public int Y { get; set; }
		}
	}
}
