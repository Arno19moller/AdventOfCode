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
            var lines = System.IO.File.ReadAllLines(@"../../../input8.txt");
            var columnCount = lines[0].Length;
            var rowCount = lines.Length;

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

                    var leftColumn = columnToConsider.Take(i);
                    var rightColumn = columnToConsider.Skip(i+1);

                    var rowArray = new List<int>();
                    for (int k = 0; k < rowCount; k++)
                    {
                        var t = lines[k].ElementAt(i);
                        rowArray.Add(int.Parse(lines[k].ElementAt(i).ToString()));
                    }

                    var topRow = rowArray.Take(i);
                    var bottomRow = rowArray.Skip(i + 1);
                    if (topRow.All(x => treeSize > x) || bottomRow.All(x => treeSize > x) || leftColumn.All(x => treeSize > x) || rightColumn.All(x => treeSize > x))
                    {
                        visibleTreeCount++;
                    }
                }
            }

            return visibleTreeCount;
        }
    }
}