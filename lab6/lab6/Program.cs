using lab6;

class Program
{
    static void Main()
    {
        // 1. Create circles using both constructors
        Circle defaultCircle = new Circle();               // radius = 1
        Circle customCircle = new Circle(5.0);             // radius = 5

        // Display initial state
        Console.WriteLine("Initial circles:");
        Console.WriteLine(defaultCircle);
        Console.WriteLine(customCircle);
        Console.WriteLine();

        // 2. Test area and circumference (already shown in ToString)
        //    We'll also compute separately for verification.
        Console.WriteLine($"Default circle area: {defaultCircle.Area():F2}");
        Console.WriteLine($"Custom circle circumference: {customCircle.Circumference():F2}");
        Console.WriteLine();

        // 3. Change radius and verify that area/circumference update
        Console.WriteLine("Changing custom circle radius to 10.0...");
        customCircle.Radius = 10.0;
        Console.WriteLine(customCircle);
        Console.WriteLine();

        // 4. Attempt to set a negative radius (should throw)
        try
        {
            Console.WriteLine("Attempting to set radius to -5...");
            customCircle.Radius = -5.0;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Exception caught: {ex.Message}");
        }

        // 5. Show unique IDs
        Circle anotherCircle = new Circle(2.5);
        Console.WriteLine($"\nAnother circle created: {anotherCircle}");
        Console.WriteLine($"IDs so far: {defaultCircle.Id}, {customCircle.Id}, {anotherCircle.Id}");

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}