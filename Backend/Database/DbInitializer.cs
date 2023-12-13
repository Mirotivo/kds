public static class DbInitializer
{
    public static void Initialize(kdsDbContext context)
    {
        context.Database.EnsureCreated();

        StationGroupSeeder.Seed(context);
        CategorySeeder.Seed(context);
        ProductSeeder.Seed(context);
    }
}