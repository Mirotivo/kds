public static class DbInitializer
{
    public static void Initialize(DBContext context)
    {
        context.Database.EnsureCreated();

        StationGroupSeeder.Seed(context);
        CategorySeeder.Seed(context);
        ProductSeeder.Seed(context);
    }
}