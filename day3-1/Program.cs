using System.Diagnostics;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;

Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();

const Int32 BufferSize = 128;

using var fileStream = File.OpenRead("data.txt");
using var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize);

string? line;
string? previousLine = null;
List<string> previousLineNumbers = new List<string>();

List<int> engineNumbers = new List<int>();


while ((line = await streamReader.ReadLineAsync()) != null)
{
    var numbers = Regex.Split(line, @"\D+");
    var originalLine = new string(line);

    if(numbers.Length == 0)
    {
        continue;
    }
    else
    {
        numbers = numbers.Where(x => !string.IsNullOrEmpty(x)).ToArray();
    }

    int previouslyDeletedCharsInLine = 0;

    //Current line
    foreach (var number in numbers)
    {
        var numIndex = line.IndexOf(number) + previouslyDeletedCharsInLine;
        var numLength = number.Length;

        //Before and after in line
        if ((numIndex != 0 && originalLine[numIndex - 1] != '.') || (numLength <= originalLine.Length - numIndex - 1 && originalLine[numIndex + numLength] != '.'))
        {
            engineNumbers.Add(int.Parse(number));
        }
        //In previous line
        else if (previousLine != null)
        {
            var startIndex = numIndex == 0 ? 0 : numIndex - 1;
            var endIndex = numIndex + numLength >= previousLine.Length ? previousLine.Length - 1 : numIndex + numLength;

            var checkedSubstring = previousLine.Substring(startIndex, endIndex - startIndex + 1);

            if (checkedSubstring.Any(subs => subs != '.'))
            {
                engineNumbers.Add(int.Parse(number));
            }
        }
        
        line = line.Substring(numIndex - previouslyDeletedCharsInLine + numLength);
        previouslyDeletedCharsInLine += numIndex + numLength - previouslyDeletedCharsInLine;
    }

    //Previous line
    if(previousLine != null)
    {
        int previouslyDeletedCharsInPreviousLine = 0;

        foreach (var number in previousLineNumbers)
        {
            var numIndex = previousLine.IndexOf(number) + previouslyDeletedCharsInPreviousLine;
            var numLength = number.Length;

            var startIndex = numIndex == 0 ? 0 : numIndex - 1;
            var endIndex = numIndex + numLength >= originalLine.Length ? originalLine.Length - 1 : numIndex + numLength;

            var checkedSubstring = originalLine.Substring(startIndex, endIndex - startIndex + 1);

            if (checkedSubstring.Any(subs => subs != '.'))
            {
                engineNumbers.Add(int.Parse(number));
            }

            previousLine = previousLine.Substring(numIndex - previouslyDeletedCharsInPreviousLine + numLength);
            previouslyDeletedCharsInPreviousLine += numIndex + numLength - previouslyDeletedCharsInPreviousLine;
        }
    }

    previousLineNumbers = new List<string>(numbers);
    previousLine = new string(originalLine);
}

stopwatch.Stop();

Console.WriteLine($"Answer: {engineNumbers.Sum()}");
Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds} ms");
Console.ReadKey();