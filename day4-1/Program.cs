using System.Text;

double sum = 0;

const Int32 BufferSize = 128;

using var fileStream = File.OpenRead("data.txt");
using var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize);

string? line;

while ((line = await streamReader.ReadLineAsync()) != null)
{
    var numbers = line.Split(':')[1];
    var numbersSeparated = numbers.Split('|');

    var winningNumbers = numbersSeparated[0].Trim().Split(' ').Where(num => !string.IsNullOrEmpty(num)).Select(num => int.Parse(num.Trim())).ToArray();
    var ticketNumbers = numbersSeparated[1].Trim().Split(' ').Where(num => !string.IsNullOrEmpty(num)).Select(num => int.Parse(num.Trim())).ToArray();

    var correspondingNumbers = winningNumbers.Intersect(ticketNumbers);

    var count = correspondingNumbers.Count();

    if(count == 0)
        continue;

    sum += Math.Pow(2, count - 1);
}

Console.WriteLine($"Sum: {sum}");
Console.ReadKey();