namespace AdventOfCode
{
	public class Day4_2021
	{
		public int Execute()
		{
			var lines = File.ReadAllLines(@"../../../2021/Inputs/day4.txt");

			var numberString = lines.ElementAt(0).Split(",");
			var bingoNumbers = numberString.Select(x => int.Parse(x)).ToList();

			var bingoBoards = new List<List<List<KeyValuePair<int, bool>>>>();
			var boardIndex = -1;
			var lineIndex = 0;
			// Set up the board
			foreach (var line in lines.Select((x, i) => new { Val = x, Index = i }))
			{
				if (line.Index < 1) continue;

				if (string.IsNullOrEmpty(line.Val))
				{
					lineIndex = 0;
					boardIndex++;
					bingoBoards.Add(new List<List<KeyValuePair<int, bool>>>());
					continue;
				}

				numberString = line.Val.Split(" ").Where(x => !string.IsNullOrEmpty(x)).ToArray();
				bingoBoards.ElementAt(boardIndex).Add(new List<KeyValuePair<int, bool>>());
				bingoBoards.ElementAt(boardIndex).ElementAt(lineIndex++).AddRange(
					numberString.Select(x => new KeyValuePair<int, bool>(int.Parse(x), false)
					).ToList());
			}

			/** Part 1 */
			// For each of the bingo numbers
			//foreach (var num in bingoNumbers)
			//{
			//	foreach (var board in bingoBoards)
			//	{
			//		 Mark bingo boards and check for bingo
			//		if (IsBingo(board, num))
			//		{
			//			var unMarked = 0;
			//			foreach (var row in board)
			//			{
			//				unMarked += row.Where(x => !x.Value)
			//					.Sum(x => x.Key);
			//			}
			//			return unMarked * num;
			//		}
			//	}
			//}

			/** Part 2 */
			foreach (var num in bingoNumbers)
			{
				foreach (var board in bingoBoards)
				{
					// Mark bingo boards
					MarkBingoBoard(board, num);
				}
			}

			bingoBoards = bingoBoards.Where(x => IsBingo(x)).ToList();

			bingoNumbers.Reverse();
			bingoBoards.Reverse();
			foreach (var num in bingoNumbers)
			{
				foreach (var board in bingoBoards)
				{
					UnMarkBingoBoard(board, num);
					if (!IsBingo(board))
					{
						MarkBingoBoard(board, num);
						var unMarked = 0;
						foreach (var row in board)
						{
							unMarked += row.Where(x => !x.Value)
								.Sum(x => x.Key);
						}
						return unMarked * num;
					}
				}
			}

			return 0;
		}

		// Part 2
		private void MarkBingoBoard(List<List<KeyValuePair<int, bool>>> board, int bingoNum)
		{
			var rowColLength = board.Count;
			for (var i = 0; i < rowColLength; i++)
			{
				for (var k = 0; k < rowColLength; k++)
				{
					if (board.ElementAt(i).ElementAt(k).Key == bingoNum)
					{
						board[i][k] = new KeyValuePair<int, bool>(bingoNum, true);
					}
				}
			}
		}

		private void UnMarkBingoBoard(List<List<KeyValuePair<int, bool>>> board, int bingoNum)
		{
			var rowColLength = board.Count;
			for (var i = 0; i < rowColLength; i++)
			{
				for (var k = 0; k < rowColLength; k++)
				{
					if (board.ElementAt(i).ElementAt(k).Key == bingoNum)
					{
						board[i][k] = new KeyValuePair<int, bool>(bingoNum, false);
					}
				}
			}
		}

		private bool IsBingo(List<List<KeyValuePair<int, bool>>> board)
		{
			var rowColLength = board.Count;

			foreach (var row in board)
			{
				var isRowBingo = row.All(x => x.Value);
				if (isRowBingo)
				{
					return true;
				}
			}

			for (var i = 0; i < rowColLength; i++)
			{
				var isColBingo = true;
				for (var k = 0; k < rowColLength; k++)
				{
					if (!board.ElementAt(k).ElementAt(i).Value)
					{
						isColBingo = false; break;
					}
				}
				if (isColBingo)
				{
					return true;
				}
			}
			return false;
		}

		// Part 1
		//private bool IsBingo(List<List<KeyValuePair<int, bool>>> board, int bingoNum)
		//{
		//	var rowColLength = board.Count;
		//	for (var i = 0; i < rowColLength; i++)
		//	{
		//		for (var k = 0; k < rowColLength; k++)
		//		{
		//			if (board.ElementAt(i).ElementAt(k).Key == bingoNum)
		//			{
		//				board[i][k] = new KeyValuePair<int, bool>(bingoNum, true);
		//			}
		//		}
		//	}

		//	foreach (var row in board)
		//	{
		//		var isRowBingo = row.All(x => x.Value);
		//		if (isRowBingo)
		//		{
		//			return true;
		//		}
		//	}

		//	for (var i = 0; i < rowColLength; i++)
		//	{
		//		var isColBingo = true;
		//		for (var k = 0; k < rowColLength; k++)
		//		{
		//			if (!board.ElementAt(k).ElementAt(i).Value)
		//			{
		//				isColBingo = false; break;
		//			}
		//		}
		//		if (isColBingo)
		//		{
		//			return true;
		//		}
		//	}
		//	return false;
		//}
	}
}
