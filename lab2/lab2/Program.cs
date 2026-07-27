using System;

class ModuleOutcomeCalculator
{
    static void Main()
    {
        // 1. Read raw marks (whole numbers)
        Console.Write("Enter Digital Portfolio mark (out of 30): ");
        int dpRaw = int.Parse(Console.ReadLine());

        Console.Write("Enter Open Book Programming Exam mark (out of 20): ");
        int examRaw = int.Parse(Console.ReadLine());

        Console.Write("Enter Capstone Project mark (out of 100): ");
        int projectRaw = int.Parse(Console.ReadLine());

        // 2. Calculate percentages, rounded to 2 decimal places
        double dpPercent = RoundTo2(100.0 * dpRaw / 30);
        double examPercent = RoundTo2(100.0 * examRaw / 20);
        double projectPercent = RoundTo2(100.0 * projectRaw / 100);

        // 3. Calculate overall module mark (weighted)
        double moduleMark = dpPercent * 0.50 + examPercent * 0.25 + projectPercent * 0.25;
        moduleMark = RoundTo2(moduleMark);

        // 4. Apply compulsory pass rule: exam and project must be >= 40%
        bool examPassed = examPercent >= 40.0;
        bool projectPassed = projectPercent >= 40.0;
        if (!examPassed || !projectPassed)
        {
            moduleMark = 34.0;   // capped at 34%
        }

        // 5. Determine classification
        string classification;
        if (moduleMark >= 70.0)
            classification = "First-Class Honours (First)";
        else if (moduleMark >= 60.0)
            classification = "Upper Second-Class Honours (2:1)";
        else if (moduleMark >= 50.0)
            classification = "Lower Second-Class Honours (2:2)";
        else if (moduleMark >= 40.0)
            classification = "Third-Class Honours (Third)";
        else
            classification = "Fail (no Honours)";

        // 6. Output the result
        Console.WriteLine($"\nModule mark: {moduleMark:F2}% - {classification}");
    }

    /// <summary>
    /// Rounds a double to two decimal places using MidpointRounding.AwayFromZero.
    /// </summary>
    static double RoundTo2(double value)
    {
        return Math.Round(value, 2, MidpointRounding.AwayFromZero);
    }
}