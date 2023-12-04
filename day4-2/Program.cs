using System.Text;

int cardsSum = 0;

const Int32 BufferSize = 128;

using var fileStream = File.OpenRead("data.txt");
using var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize);

string? line;
List<int> wonCards = new List<int>();

while ((line = await streamReader.ReadLineAsync()) != null)
{
    var gameParts = line.Split(':');

    var cardInfo = gameParts[0];
    var numbers = gameParts[1];

    var numbersSeparated = numbers.Split('|');

    var cardId = int.Parse(cardInfo.Split(' ').Where(ci => !string.IsNullOrEmpty(ci)).ToArray()[1].Trim());
    var winningNumbers = numbersSeparated[0].Trim().Split(' ').Where(num => !string.IsNullOrEmpty(num)).Select(num => int.Parse(num.Trim()));
    var ticketNumbers = numbersSeparated[1].Trim().Split(' ').Where(num => !string.IsNullOrEmpty(num)).Select(num => int.Parse(num.Trim()));
    var correspondingNumbers = winningNumbers.Intersect(ticketNumbers);
    var count = correspondingNumbers.Count();

    var currentCardSum = wonCards.Where(wc => wc == cardId).Count() + 1;
    wonCards.RemoveAll(wc => wc == cardId);

    if (count > 0)
    {
        //Append won cards
        for (int i = 1; i <= count; i++)
        {
            for (int j = 0; j < currentCardSum; j++)
            {
                wonCards.Add((cardId + i));
            }
        }
    }

    cardsSum += currentCardSum;

}

Console.WriteLine($"Sum: {cardsSum}");
Console.ReadKey();