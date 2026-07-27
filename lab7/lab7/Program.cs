using lab7;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, Mini Heroes Quest!");

        var ranger = new Ranger("John");
        var barbarian = new Barbarian("Susan");
        var mage = new Mage("Richard");

        // Allowed actions
        barbarian.SwingAxe(ranger);
        ranger.FireArrows(barbarian);
        mage.HealTarget(ranger);

        // This would be prevented (only ranger can fire arrows). The following line
        // would not compile because Barbarian has no FireArrows method, so it is
        // left commented to keep the project building.
        // barbarian.FireArrows(mage);   // not allowed

        // Additional tests
        Console.WriteLine("\n--- More tests ---");
        ranger.CollectArrows();
        mage.ThrowFireball(barbarian);
        barbarian.Rest();
        ranger.Rest();

        // Check that knocked-out characters cannot act
        while (!barbarian.IsKnockedOut)
        {
            barbarian.SwingAxe(mage);
        }
        Console.WriteLine($"{barbarian.Name} is knocked out.");
        barbarian.SwingAxe(mage);   // silently ignored (knocked out check)
    }
}