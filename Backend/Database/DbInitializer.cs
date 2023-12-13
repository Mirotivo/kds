public static class DbInitializer
{
    public static void Initialize(kdsDbContext context)
    {
        context.Database.EnsureCreated();

        Console.WriteLine($"SQLite Database: Seeding ...");

        StationGroupSeeder.Seed(context);
        CategorySeeder.Seed(context);
        ProductSeeder.Seed(context);
    }
}