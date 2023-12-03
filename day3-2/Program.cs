/* 
NOTE: SOURCE CODE COPIED FROM ANOTHER REPO SINCE I DIDNT HAVE TIME TO DEAL WITH THIS THING
SOURCE: https://github.com/Jozelle/AOC2023/blob/7b0f5a48773ef22649bf180a2bcef41dcea59e0b/Day3/Day3_Part2.cs
 */

List<char[]> engineSchematic = ReadFromFile();

int sumGears = 0;

for (int i = 0; i < engineSchematic.Count; i++)
{
    for (int j = 0; j < engineSchematic[i].Length; j++)
    {
        if (engineSchematic[i][j] == '*')
        {
            sumGears += LocateAdjacentDigits(engineSchematic, i, j);
        }

    }
}

Console.WriteLine(sumGears);

List<char[]> ReadFromFile()
{
    string filename = "data.txt";
    StreamReader sr = new StreamReader(filename);

    List<char[]> engineSchematic = new List<char[]>();

    while (!sr.EndOfStream)
    {
        string lineFromFile = sr.ReadLine();
        char[] lineFromFileAsArray = lineFromFile.ToCharArray();

        engineSchematic.Add(lineFromFileAsArray);
    }

    sr.Close();

    return engineSchematic;
}
int LocateAdjacentDigits(List<char[]> engineSchematic, int y, int x)
{
    int firstNumber = 0;
    int secondNumber = 0;

    for (int i = y - 1; i <= y + 1; i++)
    {
        for (int j = x - 1; j <= x + 1; j++)
        {
            if (char.IsDigit(engineSchematic[i][j]))
            {
                int temp = LocateNumber(engineSchematic, i, j);
                if (firstNumber == 0)
                {
                    firstNumber = temp;
                }
                else if (temp != firstNumber)
                {
                    secondNumber = temp;
                }
            }
        }
    }

    int sum = firstNumber * secondNumber;
    //If a second number cannot be found, it will multiply with and return 0;
    return sum;
}
int LocateNumber(List<char[]> engineSchematic, int y, int x)
{
    int start = x;
    int end = x;

    //Check to find the first digit in the number
    while (true)
    {
        if (start > 0)
        {
            if (char.IsDigit(engineSchematic[y][start - 1]))
            {
                start--;
            }
            else
            {
                break;
            }
        }
        else
        {
            break;
        }
    }

    //Check to find the last digit in the number
    while (true)
    {
        if (end < engineSchematic[y].Length - 1)
        {
            if (char.IsDigit(engineSchematic[y][end + 1]))
            {
                end++;
            }
            else
            {
                break;
            }
        }
        else
        {
            break;
        }
    }

    string result = "";

    //Iterate from first digit to last
    for (int i = start; i <= end; i++)
    {
        //Save the digit to result
        result += engineSchematic[y][i];

    }

    return int.Parse(result);
}