using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day1_2023
    {
        private List<int> total = new List<int>();

        public int Execute()
        {
            var lines = System.IO.File.ReadAllLines(@"../../../input9.txt");
            var rowCount = lines.Length;
            int[] calibrations = new int[rowCount];

			foreach (var line in lines.Select((value, i) => new { value, i }))
            {
                var firstNum = "";
                var lastNum = "";
                var indexToLookFrom = 0;
                foreach (var charr in line.value.Select((value, i) => new { value, i }))
                {
                    var isNum = int.TryParse(charr.value.ToString(), out var num);

					var substring = line.value.Substring(indexToLookFrom, charr.i - indexToLookFrom + 1);


					if (substring.EndsWith("one"))
                    {
						if (firstNum == "")
						{
							firstNum = "1";
						}
						lastNum = "1";
						indexToLookFrom = charr.i;
					}
                    else if (substring.Contains("two"))
                    {
						if (firstNum == "")
						{
							firstNum = "2";
						}
						lastNum = "2";
						indexToLookFrom = charr.i;
					}
                    else if (substring.Contains("three"))
                    {
						if (firstNum == "")
						{
							firstNum = "3";
						}
						lastNum = "3";
						indexToLookFrom = charr.i;
					}
                    else if (substring.Contains("four"))
                    {
						if (firstNum == "")
						{
							firstNum = "4";
						}
						lastNum = "4";
						indexToLookFrom = charr.i;
					}
                    else if (substring.Contains("five"))
                    {
						if (firstNum == "")
						{
							firstNum = "5";
						}
						lastNum = "5";
						indexToLookFrom = charr.i;
					}
                    else if (substring.Contains("six"))
                    {
						if (firstNum == "")
						{
							firstNum = "6";
						}
						lastNum = "6";
						indexToLookFrom = charr.i;
					}
                    else if (substring.Contains("seven"))
                    {
						if (firstNum == "")
						{
							firstNum = "7";
						}
						lastNum = "7";
						indexToLookFrom = charr.i;
					}
                    else if (substring.Contains("eight"))
                    {
						if (firstNum == "")
						{
							firstNum = "8";
						}
						lastNum = "8";
						indexToLookFrom = charr.i;
					}
                    else if (substring.Contains("nine"))
                    {
						if (firstNum == "")
						{
							firstNum = "9";
						}
						lastNum = "9";
						indexToLookFrom = charr.i;
					}

                    if (isNum)
                    {
                        if (firstNum == "")
                        {
                            firstNum = num.ToString();
                        }
                        lastNum = num.ToString();
                    }
                }
                calibrations[line.i] = int.Parse(firstNum + lastNum);
				//Console.WriteLine(calibrations[line.i]);

			}

            return calibrations.Sum();
        }
    }
}