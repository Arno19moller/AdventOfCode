using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day8
    {
        private List<int> total = new List<int>();

        public int Execute()
        {
            var lines = System.IO.File.ReadAllLines(@"../../../2022/Inputs/input8.txt");
            var columnCount = lines[0].Length;
            var rowCount = lines.Length;
            var mostScenic = new List<int>();

            var visibleTreeCount = (columnCount * 2) + ((rowCount - 2) * 2);

            foreach (var line in lines.Select((value, i) => new { value, i}))
            {
                if (line.i == 0 || line.i == rowCount - 1)
                {
                    continue;
                }

                for (var i = 1; i < columnCount - 1; i++)
                {
                    var treeSize = int.Parse(line.value.ElementAt(i).ToString());

                    var columnArray = line.value.ToCharArray();
                    var columnToConsider = new List<int>();
                    foreach (var rowTree in columnArray.Select((value, i) => new { value, i}))
                    {
                        columnToConsider.Add(int.Parse(rowTree.value.ToString()));
                    }

                    var leftColumn = columnToConsider.Take(i).ToList();
                    leftColumn.Reverse();
                    var rightColumn = columnToConsider.Skip(i+1).ToList();

                    var rowArray = new List<int>();
                    for (int k = 0; k < rowCount; k++)
                    {
                        var t = lines[k].ElementAt(i);
                        rowArray.Add(int.Parse(lines[k].ElementAt(i).ToString()));
                    }

                    var topRow = rowArray.Take(line.i).ToList();
                    topRow.Reverse();
                    var bottomRow = rowArray.Skip(line.i + 1).ToList();

                    //Part 1
                    //if (topRow.All(x => treeSize > x) || bottomRow.All(x => treeSize > x) || leftColumn.All(x => treeSize > x) || rightColumn.All(x => treeSize > x))
                    //{
                    //    visibleTreeCount++;
                    //}

                    //Part 2
                    var topScore =  topRow.FindIndex(x => x >= treeSize) == -1 ? topRow.Count() : (1 + topRow.FindIndex(x => x >= treeSize));
                    var bottomScore = bottomRow.FindIndex(x => x >= treeSize) == -1 ? bottomRow.Count() : (1 + bottomRow.FindIndex(x => x >= treeSize));
                    var leftScore = leftColumn.FindIndex(x => x >= treeSize) == -1 ? leftColumn.Count() : (1 + leftColumn.FindIndex(x => x >= treeSize));
                    var rightScore = rightColumn.FindIndex(x => x >= treeSize) == -1 ? rightColumn.Count() : (1 + rightColumn.FindIndex(x => x >= treeSize));

                    mostScenic.Add(topScore * bottomScore * leftScore * rightScore);
                }
            }

            mostScenic.Sort();
            mostScenic.Reverse();

            //Part 1
            //return visibleTreeCount;

            //Part 2
            return mostScenic[0];
        }
    }
}