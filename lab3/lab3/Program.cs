using System;
using System.IO;

class OysterFestival
{
    static void Main()
    {
        // 1. Ask for number of competitors
        Console.Write("Enter the number of competitors: ");
        int count = int.Parse(Console.ReadLine());

        // Create parallel arrays
        string[] names = new string[count];
        int[] scores = new int[count];

        // 2. Read each competitor's name and score
        for (int i = 0; i < count; i++)
        {
            Console.Write($"Enter competitor {i + 1} name: ");
            names[i] = Console.ReadLine();
            Console.Write($"Enter oysters eaten by {names[i]}: ");
            scores[i] = int.Parse(Console.ReadLine());
        }

        // 3. Sort descending by score using bubble sort (custom)
        BubbleSortDescending(names, scores);

        // 4. Output to Console and build output lines for file
        string[] outputLines = new string[count];
        Console.WriteLine("\nResults:");
        for (int i = 0; i < count; i++)
        {
            string line = $"{i + 1}. {names[i]} ate {scores[i]} oysters";
            Console.WriteLine(line);
            outputLines[i] = line;
        }

        // 5. Write to file "results.txt"
        File.WriteAllLines("results.txt", outputLines);
        Console.WriteLine("\nResults saved to results.txt");
    }

    /// <summary>
    /// Sorts two parallel arrays (names and scores) in descending order of scores.
    /// Uses a simple bubble sort.
    /// </summary>
    static void BubbleSortDescending(string[] names, int[] scores)
    {
        int n = scores.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                // Swap if current score is less than next (descending)
                if (scores[j] < scores[j + 1])
                {
                    // Swap scores
                    int tempScore = scores[j];
                    scores[j] = scores[j + 1];
                    scores[j + 1] = tempScore;

                    // Swap names in sync
                    string tempName = names[j];
                    names[j] = names[j + 1];
                    names[j + 1] = tempName;
                }
            }
        }
    }
}