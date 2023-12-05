namespace AdventOfCode
{
    public class Day4_2023
    {
        public int Execute()
        {
            var lines = File.ReadAllLines(@"../../../2023/Inputs/input4.txt");
            var winningNumbers = new List<int>();
            var myCardNumbers = new List<int>();
            // Part 1
            //var totalCardWorth = 0;

            // Part 2
            var totalCards = 0;
            var duplicateCards = new List<int>();

            // Part 1
            foreach (var line in lines.Select((value, i) => new { value, i }))
            {
				// Part 2
				if (duplicateCards.Count() == 0 || line.i >= duplicateCards.Count())
                    duplicateCards.Add(1);

				var numberSplit = line.value.Substring(line.value.IndexOf(":") + 1).Split('|');
                numberSplit[0] = numberSplit[0].Trim();
                numberSplit[1] = numberSplit[1].Trim();

                var winningCardNums = numberSplit[0].Split(" ");
                var myCardNums = numberSplit[1].Split(" ");

                foreach (var cardNumber in winningCardNums)
                {
                    if (int.TryParse(cardNumber, out var cn))
                    {
                        winningNumbers.Add(cn);
                    }
                }

                foreach (var cardNumber in myCardNums)
                {
                    if (int.TryParse(cardNumber, out var cn))
                    {
                        myCardNumbers.Add(cn);
                    }
                }

                var matchingNumCount = winningNumbers.Where(x => myCardNumbers.Contains(x)).Count();

                // Part 2
                for (var p = 0; p < duplicateCards[line.i]; p++)
                {
                    for (var i = line.i + 1; i < line.i + 1 + matchingNumCount; i++)
                    {
                        if (i >= duplicateCards.Count())
                            duplicateCards.Add(1);

                        duplicateCards[i]++;
                    }
                }

                totalCards += duplicateCards[line.i];

				// Part 1
				//var cardWorth = matchingNumCount > 0 ? 1 : 0;
				//for(var i = 0; i < matchingNumCount - 1; i++)
				//{
				//    cardWorth *= 2;
				//}
				//totalCardWorth += cardWorth;

				winningNumbers.Clear();
                myCardNumbers.Clear();
            }

            // Part 1
            //return totalCardWorth;
            return totalCards;
        }

        internal class DuplicateCard
        {
            public string Card { get; set; }
            public int Count { get; set; }
        }
    }
}