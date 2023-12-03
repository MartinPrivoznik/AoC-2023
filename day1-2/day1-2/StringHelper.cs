using System.Linq;

namespace day1_2
{
    public class StringHelper
    {
        public FirstLastNumber? GetNumberPair(string input)
        {
            int? firstNumber = null;
            int? lastNumber = null;


            for (int i = 0; i < input.Length; i++)
            {
                var character = input[i];

                if (Char.IsDigit(input[i]))
                {
                    if (firstNumber is null)
                    {
                        firstNumber = input[i] - '0';
                    }

                    lastNumber = input[i] - '0';
                }
                else
                {
                    try
                    {
                        var correspondingPair = NumberPairs.FirstOrDefault(np => 
                            np.Key.Length <= input.Length - i &&
                            np.Key.Equals(input.ToLower().Substring(i, np.Key.Length))
                        );
                        
                        if (correspondingPair.Equals(default(KeyValuePair<string, int>)))
                        {
                            continue;
                        }

                        if (firstNumber is null)
                        {
                            firstNumber = correspondingPair.Value;
                        }

                        lastNumber = correspondingPair.Value;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

            return new FirstLastNumber
            {
                First = firstNumber,
                Last = lastNumber
            };
        }

        public class FirstLastNumber
        {
            public int? First { get; set; }
            public int? Last { get; set; }
        }

        public static Dictionary<string, int> NumberPairs = new Dictionary<string, int>
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 },
        };
    }
}
