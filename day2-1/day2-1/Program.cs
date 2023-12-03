using System.Diagnostics;
using System.Text;

Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();

const Int32 BufferSize = 128;

using var fileStream = File.OpenRead("data.txt");
using var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize);

string? line;

var possibleGameIds = new List<int>();

while ((line = await streamReader.ReadLineAsync()) != null)
{
    var gamePair = line.Split(':');
    var gameId = gamePair[0];
    var gameValues = gamePair[1];

    var gameOptions = gameValues.Split(';');

    var combinations = gameOptions.SelectMany(go => go.Split(',').Select(g => g.Trim()));

    var maxBlueOption = combinations.Where(c => c.Contains("blue")).Select(c => int.Parse(c.Split(' ')[0])).Max();
    var maxRedOption = combinations.Where(c => c.Contains("red")).Select(c => int.Parse(c.Split(' ')[0])).Max();
    var maxGreenOption = combinations.Where(c => c.Contains("green")).Select(c => int.Parse(c.Split(' ')[0])).Max();

    if (maxBlueOption <= 14 && maxRedOption <= 12 && maxGreenOption <= 13)
    {
        possibleGameIds.Add(int.Parse(gameId.Split(' ')[1]));
    }
}

stopwatch.Stop();

Console.WriteLine($"Answer: {possibleGameIds.Sum()}");
Console.ReadKey();