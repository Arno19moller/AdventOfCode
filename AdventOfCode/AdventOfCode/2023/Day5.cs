using System.Diagnostics.Metrics;

namespace AdventOfCode
{
    public class Day5_2023
    {
        public long Execute()
        {
            var lines = File.ReadAllLines(@"../../../2023/Inputs/input5.txt");
            var seedToSoil = new List<Map>();
            var soilToFertilizer = new List<Map>();
            var fertiliserToWater = new List<Map>();
            var waterToLight = new List<Map>();
            var lightToTemp = new List<Map>();
            var tempToHumidity = new List<Map>();
            var humidityToLocation = new List<Map>();

            var seeds = new List<long>();
			var type = "";
			var lowestLocation = 999999999999999999;

            foreach (var line in lines.Select((value, i) => new { value, i }))
            {
                if (line.value.StartsWith("seeds:"))
                {
                    var seedsString = line.value.Substring(line.value.IndexOf(":") + 2);
                    var seedArr = seedsString.Split(' ');
                    seeds = seedArr.Select(x => long.Parse(x)).ToList();
					continue;

				}
				else if (line.value.Equals(string.Empty))
				{
					continue;
				}

				switch (line.value)
				{
					case "seed-to-soil map:":
						type = "sts";
						continue;
					case "soil-to-fertilizer map:":
						type = "stf";
						continue;
					case "fertilizer-to-water map:":
						type = "ftw";
						continue;
					case "water-to-light map:":
						type = "wtl";
						continue;
					case "light-to-temperature map:":
						type = "ltt";
						continue;
					case "temperature-to-humidity map:":
						type = "tth";
						continue;
					case "humidity-to-location map:":
						type = "htl";
						continue;
				}

				var ratioArr = line.value.Split(' ');
				var ratioValues = ratioArr.Select(x => long.Parse(x)).ToList();

				switch (type)
				{
					case "sts":
						seedToSoil.Add(new Map() { Source = ratioValues[1], Destination = ratioValues[0], Count = ratioValues[2] });
						break;
					case "stf":
						soilToFertilizer.Add(new Map() { Source = ratioValues[1], Destination = ratioValues[0], Count = ratioValues[2] });
						break;
					case "ftw":
						fertiliserToWater.Add(new Map() { Source = ratioValues[1], Destination = ratioValues[0], Count = ratioValues[2] });
						break;
					case "wtl":
						waterToLight.Add(new Map() { Source = ratioValues[1], Destination = ratioValues[0], Count = ratioValues[2] });
						break;
					case "ltt":
						lightToTemp.Add(new Map() { Source = ratioValues[1], Destination = ratioValues[0], Count = ratioValues[2] });
						break;
					case "tth":
						tempToHumidity.Add(new Map() { Source = ratioValues[1], Destination = ratioValues[0], Count = ratioValues[2] });
						break;
					case "htl":
						humidityToLocation.Add(new Map() { Source = ratioValues[1], Destination = ratioValues[0], Count = ratioValues[2] });
						break;
				}


				var k = 0;
			}

			// Part 2
			for (int i = 0; i < seedLength; i += 2)
			{
				for (int k = 0; k < seeds[i + 1]; k++)
				{
					var soilMatch = seedToSoil.FirstOrDefault(s => seeds[i] + k >= s.Source && seeds[i] + k <= s.Source + s.Count);
					var soil = seeds[i] + k;
					if (soilMatch is not null)
					{
						var soilDifference = seeds[i] + k - soilMatch.Source;
						soil = soilMatch.Destination + soilDifference;
					}

					var fertilizerMatch = soilToFertilizer.FirstOrDefault(s => soil >= s.Source && soil <= s.Source + s.Count);
					var fertilizer = soil;
					if (fertilizerMatch is not null)
					{
						var fertilizerDifference = soil - fertilizerMatch.Source;
						fertilizer = fertilizerMatch.Destination + fertilizerDifference;
					}

					var waterMatch = fertiliserToWater.FirstOrDefault(s => fertilizer >= s.Source && fertilizer <= s.Source + s.Count);
					var water = fertilizer;
					if (waterMatch is not null)
					{
						var waterDifference = fertilizer - waterMatch.Source;
						water = waterMatch.Destination + waterDifference;
					}

					var lightMatch = waterToLight.FirstOrDefault(s => water >= s.Source && water <= s.Source + s.Count);
					var light = water;
					if (lightMatch is not null)
					{
						var lightDifference = water - lightMatch.Source;
						light = lightMatch.Destination + lightDifference;
					}

					var tempMatch = lightToTemp.FirstOrDefault(s => light >= s.Source && light <= s.Source + s.Count);
					var temp = light;
					if (tempMatch is not null)
					{
						var tempDifference = light - tempMatch.Source;
						temp = tempMatch.Destination + tempDifference;
					}

					var humidityMatch = tempToHumidity.FirstOrDefault(s => temp >= s.Source && temp <= s.Source + s.Count);
					var humidity = temp;
					if (humidityMatch is not null)
					{
						var humidityDifference = temp - humidityMatch.Source;
						humidity = humidityMatch.Destination + humidityDifference;
					}

					var locationMatch = humidityToLocation.FirstOrDefault(s => humidity >= s.Source && humidity <= s.Source + s.Count);
					var location = humidity;
					if (locationMatch is not null)
					{
						var locationDifference = humidity - locationMatch.Source;
						location = locationMatch.Destination + locationDifference;
					}

					lowestLocation = (long)(location < lowestLocation ? location : lowestLocation);
				}
			}

			// Part 1
			//foreach(var seed in seeds)
			//{
			//	var soilMatch = seedToSoil.FirstOrDefault(s => seed >= s.Source && seed <= s.Source + s.Count);
			//	var soil = seed;
			//	if (soilMatch is not null)
			//	{
			//		var soilDifference = seed - soilMatch.Source;
			//		soil = soilMatch.Destination + soilDifference;
			//	}

			//	var fertilizerMatch = soilToFertilizer.FirstOrDefault(s => soil >= s.Source && soil <= s.Source + s.Count);
			//	var fertilizer = soil;
			//	if (fertilizerMatch is not null)
			//	{
			//		var fertilizerDifference = soil - fertilizerMatch.Source;
			//		fertilizer = fertilizerMatch.Destination + fertilizerDifference;
			//	}

			//	var waterMatch = fertiliserToWater.FirstOrDefault(s => fertilizer >= s.Source && fertilizer <= s.Source + s.Count);
			//	var water = fertilizer;
			//	if (waterMatch is not null)
			//	{
			//		var waterDifference = fertilizer - waterMatch.Source;
			//		water = waterMatch.Destination + waterDifference;
			//	}

			//	var lightMatch = waterToLight.FirstOrDefault(s => water >= s.Source && water <= s.Source + s.Count);
			//	var light = water;
			//	if (lightMatch is not null)
			//	{
			//		var lightDifference = water - lightMatch.Source;
			//		light = lightMatch.Destination + lightDifference;
			//	}

			//	var tempMatch = lightToTemp.FirstOrDefault(s => light >= s.Source && light <= s.Source + s.Count);
			//	var temp = light;
			//	if (tempMatch is not null)
			//	{
			//		var tempDifference = light - tempMatch.Source;
			//		temp = tempMatch.Destination + tempDifference;
			//	}

			//	var humidityMatch = tempToHumidity.FirstOrDefault(s => temp >= s.Source && temp <= s.Source + s.Count);
			//	var humidity = temp;
			//	if (humidityMatch is not null)
			//	{
			//		var humidityDifference = temp - humidityMatch.Source;
			//		humidity = humidityMatch.Destination + humidityDifference;
			//	}

			//	var locationMatch = humidityToLocation.FirstOrDefault(s => humidity >= s.Source && humidity <= s.Source + s.Count);
			//	var location = humidity;
			//	if (locationMatch is not null)
			//	{
			//		var locationDifference = humidity - locationMatch.Source;
			//		location = locationMatch.Destination + locationDifference;
			//	}

			//	lowestLocation = (long)(location < lowestLocation ? location : lowestLocation);
			//}

			return lowestLocation;
        }

		internal class Map
		{
			public long Source { get; set; }
			public long Destination { get; set; }
			public long Count { get; set; }
		}
	}
}