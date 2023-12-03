using day1_2;
using System.Diagnostics;
using System.Text;

Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();

var sum = 0;

const Int32 BufferSize = 128;

using var fileStream = File.OpenRead("data.txt");
using var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize);

string? line;

while ((line = await streamReader.ReadLineAsync()) != null)
{
    var helper = new StringHelper();
    var pair = helper.GetNumberPair(line);

    if (pair is null)
    {
        continue;
    }

    sum += Int32.Parse($"{pair.First}{pair.Last}");
}

stopwatch.Stop();

Console.WriteLine($"Sum: {sum}");
Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds} MS");
Console.ReadKey();