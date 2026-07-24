using System;

class CarHireProgram
{
    static void Main()
    {
        // Ask for the number of days hired
        Console.Write("How many days was the car hired for? ");
        int days = int.Parse(Console.ReadLine());

        // Ask for the litres of fuel left in the tank
        Console.Write("How many litres of fuel were left in the tank? ");
        double litresLeft = double.Parse(Console.ReadLine());

        // Tank capacity is 50 litres
        double petrolUsed = 50 - litresLeft;

        // Calculate total cost
        double total = days * 25 + petrolUsed * 2.5 + 10;

        // Print the total, removing ".0" if the value is a whole number
        if (total % 1 == 0)
            Console.WriteLine($"The total charge for the car hire is £{total:F0}");
        else
            Console.WriteLine($"The total charge for the car hire is £{total}");
    }
}