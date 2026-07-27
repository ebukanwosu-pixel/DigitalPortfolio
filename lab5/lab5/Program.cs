using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to the RPG Character Creator");

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Create a new character.");
            Console.WriteLine("2. Load an existing character.");
            Console.WriteLine("3. Exit.");

            int selection = ReadIntInRange(1, 3, "Enter selection (1-3):");
            if (selection == 3) break;

            if (selection == 1)
            {
                CreateNewCharacter();
            }
            else if (selection == 2)
            {
                LoadExistingCharacter();
            }
        }

        Console.WriteLine("Thank you for using the RPG Character Creator");
        Console.WriteLine("Goodbye");
    }

    // ---------- Helper Methods ----------

    static int ReadIntInRange(int min, int max, string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            try
            {
                var raw = Console.ReadLine();
                if (raw is null) continue;
                int value = int.Parse(raw);
                if (value < min || value > max)
                {
                    Console.WriteLine($"Please enter a number between {min} and {max}.");
                    continue;
                }
                return value;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid number format. Please enter an integer.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Number is too large. Please enter a valid integer.");
            }
        }
    }

    static (string creatureType, int hp, int str, int mag, int spd) GetCreatureStats(int choice, int baseHp, int baseStr, int baseMag, int baseSpd)
    {
        // Copy base values to avoid modifying input references
        int hp = baseHp;
        int str = baseStr;
        int mag = baseMag;
        int spd = baseSpd;
        string creatureType;

        switch (choice)
        {
            case 1: // Human
                creatureType = "Human";
                hp += 70;
                str += 50;
                mag += 10;
                spd += 30;
                break;
            case 2: // Elf
                creatureType = "Elvish";
                hp += 50;
                str += 30;
                mag += 40;
                spd += 50;
                break;
            case 3: // Dwarf
                creatureType = "Dwarvish";
                hp += 100;
                str += 80;
                mag += 10;
                spd += 10;
                break;
            case 4: // Goblin
                creatureType = "Goblin";
                hp += 10;
                str += 10;
                mag += 10;
                spd += 40;
                break;
            case 5: // Orc
                creatureType = "Orcish";
                hp += 120;
                str += 100;
                // Magic unchanged
                spd += 20;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(choice), "Invalid creature choice.");
        }

        return (creatureType, hp, str, mag, spd);
    }

    static (string characterClass, int hp, int str, int mag, int spd) GetClassStats(int choice, int baseHp, int baseStr, int baseMag, int baseSpd)
    {
        int hp = baseHp;
        int str = baseStr;
        int mag = baseMag;
        int spd = baseSpd;
        string characterClass;

        switch (choice)
        {
            case 1: // Warrior
                characterClass = "Warrior";
                str += 50;
                hp += 50;
                break;
            case 2: // Wizard
                characterClass = "Wizard";
                mag += 100;
                break;
            case 3: // Rogue
                characterClass = "Rogue";
                hp += 20;
                mag += 30;
                spd += 50;
                break;
            case 4: // Bard
                characterClass = "Bard";
                hp += 20;
                mag += 50;
                spd += 30;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(choice), "Invalid class choice.");
        }

        return (characterClass, hp, str, mag, spd);
    }

    static void AllocateBonusPoints(ref int hp, ref int str, ref int mag, ref int spd, int totalBonus)
    {
        int remaining = totalBonus;
        int add;

        // Hit Points
        add = ReadIntInRange(0, remaining, $"You have {remaining} bonus points. How many points would you like to add to Hit Points? (0-{remaining})");
        hp += add;
        remaining -= add;

        // Strength
        if (remaining > 0)
        {
            add = ReadIntInRange(0, remaining, $"You have {remaining} bonus points. How many points would you like to add to Strength? (0-{remaining})");
            str += add;
            remaining -= add;
        }

        // Magic
        if (remaining > 0)
        {
            add = ReadIntInRange(0, remaining, $"You have {remaining} bonus points. How many points would you like to add to Magic? (0-{remaining}). Any remaining points will be added to Speed.");
            mag += add;
            remaining -= add;
            // All leftover goes to Speed
            spd += remaining;
            remaining = 0;
        }
    }

    static void DisplayCharacter(string name, string creatureType, string characterClass, int hp, int str, int mag, int spd)
    {
        Console.WriteLine();
        Console.WriteLine("Character created:");
        Console.WriteLine($"Name: {name}");
        Console.WriteLine($"Creature: {creatureType}");
        Console.WriteLine($"Class: {characterClass}");
        Console.WriteLine($"HP: {hp}");
        Console.WriteLine($"Strength: {str}");
        Console.WriteLine($"Magic: {mag}");
        Console.WriteLine($"Speed: {spd}");
    }

    static void SaveCharacter(string name, string creatureType, string characterClass, int hp, int str, int mag, int spd)
    {
        var safeName = string.Concat(name.Split(Path.GetInvalidFileNameChars()));
        var filename = Path.Combine(Directory.GetCurrentDirectory(), safeName + ".character");
        try
        {
            File.WriteAllLines(filename, new[] {
                $"Name - {name}",
                $"Type - {creatureType}",
                $"Class - {characterClass}",
                $"Stats - {hp} {str} {mag} {spd}"
            });
            Console.WriteLine($"Character saved to {Path.GetFileName(filename)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save character: {ex.Message}");
        }
    }

    static void LoadExistingCharacter()
    {
        string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.character");
        if (files.Length == 0)
        {
            Console.WriteLine("There are no character files to load.");
            return;
        }

        Console.WriteLine("Select a file to load:");
        for (int i = 0; i < files.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {Path.GetFileName(files[i])}");
        }
        int choice = ReadIntInRange(1, files.Length, $"Enter selection (1-{files.Length}):");
        int index = choice - 1;
        try
        {
            var lines = File.ReadAllLines(files[index]);
            Console.WriteLine("Loaded character data:");
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load character: {ex.Message}");
        }
    }

    static void CreateNewCharacter()
    {
        // --- Get name ---
        Console.WriteLine("What is your character's name?");
        string name = Console.ReadLine() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(name)) name = "Unknown";

        // --- Creature selection ---
        Console.WriteLine("What is your character's creature type?");
        Console.WriteLine("1. Human");
        Console.WriteLine("2. Elvish");
        Console.WriteLine("3. Dwarvish");
        Console.WriteLine("4. Goblin");
        Console.WriteLine("5. Orcish");
        int creatureChoice = ReadIntInRange(1, 5, "Enter a number between 1 and 5 inclusive:");

        // Base stats
        int hp = 20;
        int str = 20;
        int mag = 10;
        int spd = 20;

        // Apply creature stats
        (string creatureType, hp, str, mag, spd) = GetCreatureStats(creatureChoice, hp, str, mag, spd);

        // --- Class selection ---
        Console.WriteLine("What is your character's class type?");
        Console.WriteLine("1. Warrior");
        Console.WriteLine("2. Wizard");
        Console.WriteLine("3. Rogue");
        Console.WriteLine("4. Bard");
        int classChoice = ReadIntInRange(1, 4, "Enter a number between 1 and 4 inclusive:");

        // Apply class stats
        (string characterClass, hp, str, mag, spd) = GetClassStats(classChoice, hp, str, mag, spd);

        // --- Bonus point allocation ---
        AllocateBonusPoints(ref hp, ref str, ref mag, ref spd, 30);

        // --- Display and save ---
        DisplayCharacter(name, creatureType, characterClass, hp, str, mag, spd);
        SaveCharacter(name, creatureType, characterClass, hp, str, mag, spd);
    }
}