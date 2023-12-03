
using System.Text;

var sum = 0;

const Int32 BufferSize = 128;

using var fileStream = File.OpenRead("data.txt");
using var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize);

string? line;

while ((line = await streamReader.ReadLineAsync()) != null)
{
    var wordDigits = line.Where(c => Char.IsDigit(c)).ToArray();

    if(wordDigits.Length == 0)
    {
        continue;
    }

    sum += wordDigits.Length == 1 ? Int32.Parse($"{wordDigits[0]}{wordDigits[0]}") : Int32.Parse($"{wordDigits[0]}{wordDigits[wordDigits.Length - 1]}");
}

Console.WriteLine($"Sum: {sum}");
Console.ReadKey();