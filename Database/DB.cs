
public class DB
{
    public List<Category> ListCategories() {
        var categories = new List<Category>
        {
            new Category
            {
                ID = 0,
                Name = "Beverages"
            },
            new Category
            {
                ID = 1,
                Name = "Food"
            },
        };
        return categories;
    }
    public List<Product> ListProducts(string query)
    {
        var categories = ListCategories();
        var products = new List<Product>
        {
            new Product
            {
                Url = "#",
                IconClass = "icon-orange",
                Name = "Orange Juice",
                ImageUrl = "images/orange_juice.jpg",
                CategoryID = 0,
                Category = categories[0],
                Title = "Fresh Orange Juice",
                Description = "Enjoy the refreshing taste of freshly squeezed orange juice. Made from the finest oranges, this juice is packed with vitamin C and perfect for a healthy start to your day.",
                Price = 3.99
            },
            new Product
            {
                Url = "#",
                IconClass = "icon-strawberry",
                Name = "Strawberry Smoothie",
                ImageUrl = "images/strawberry_smoothie.jpg",
                CategoryID = 0,
                Category = categories[0],
                Title = "Strawberry Bliss Smoothie",
                Description = "Indulge in the creamy and delicious Strawberry Bliss Smoothie. It's a perfect blend of fresh strawberries, yogurt, and a touch of honey for sweetness.",
                Price = 4.99
            },
            new Product
            {
                Url = "#",
                IconClass = "icon-banana",
                Name = "Banana Shake",
                ImageUrl = "images/banana_shake.jpg",
                CategoryID = 0,
                Category = categories[0],
                Title = "Creamy Banana Shake",
                Description = "Savor the rich and creamy goodness of our Banana Shake. Made with ripe bananas and a hint of vanilla, it's a delightful treat for banana lovers.",
                Price = 4.49
            },
            new Product
            {
                Url = "#",
                IconClass = "icon-lemon",
                Name = "Lemonade",
                ImageUrl = "images/lemonade.jpg",
                CategoryID = 0,
                Category = categories[0],
                Title = "Classic Lemonade",
                Description = "Quench your thirst with our Classic Lemonade. It's a timeless favorite made from freshly squeezed lemons, sugar, and a splash of cool water.",
                Price = 2.99
            },
            new Product
                {
                    Url = "#",
                    IconClass = "icon-coffee",
                    Name = "Espresso",
                    ImageUrl = "images/espresso.jpg",
                    CategoryID = 0,
                    Category = categories[0],
                    Title = "Italian Espresso",
                    Description = "Experience the bold and intense flavors of our Italian Espresso. Made from finely ground coffee beans, it's a perfect pick-me-up for coffee enthusiasts.",
                    Price = 2.49
                },
                new Product
                {
                    Url = "#",
                    IconClass = "icon-tea",
                    Name = "Green Tea",
                    ImageUrl = "images/green_tea.jpg",
                    CategoryID = 0,
                    Category = categories[0],
                    Title = "Organic Green Tea",
                    Description = "Enjoy the soothing and healthful benefits of our Organic Green Tea. It's made from handpicked tea leaves and is rich in antioxidants.",
                    Price = 3.29
                },
                new Product
                {
                    Url = "#",
                    IconClass = "icon-milkshake",
                    Name = "Chocolate Milkshake",
                    ImageUrl = "images/chocolate_milkshake.jpg",
                    CategoryID = 0,
                    Category = categories[0],
                    Title = "Decadent Chocolate Milkshake",
                    Description = "Indulge in the creamy goodness of our Decadent Chocolate Milkshake. It's a chocolate lover's dream, topped with whipped cream and chocolate shavings.",
                    Price = 4.99
                },
                new Product
                {
                    Url = "#",
                    IconClass = "icon-smoothie",
                    Name = "Tropical Smoothie",
                    ImageUrl = "images/tropical_smoothie.jpg",
                    CategoryID = 0,
                    Category = categories[0],
                    Title = "Exotic Tropical Smoothie",
                    Description = "Escape to the tropics with our Exotic Tropical Smoothie. It's a delightful blend of pineapple, mango, and coconut milk, bringing the taste of paradise to your glass.",
                    Price = 5.49
                },
                new Product
                {
                    Url = "#",
                    IconClass = "icon-pizza",
                    Name = "Margherita Pizza",
                    ImageUrl = "images/margherita_pizza.jpg",
                    CategoryID = 1,
                    Category = categories[1],
                    Title = "Classic Margherita Pizza",
                    Description = "Savor the simplicity of our Classic Margherita Pizza. It's topped with fresh tomatoes, mozzarella cheese, basil leaves, and a drizzle of olive oil.",
                    Price = 9.99
                },
                new Product
                {
                    Url = "#",
                    IconClass = "icon-burger",
                    Name = "Cheeseburger",
                    ImageUrl = "images/cheeseburger.jpg",
                    CategoryID = 1,
                    Category = categories[1],
                    Title = "Cheesy Deluxe Burger",
                    Description = "Indulge in the deliciousness of our Cheesy Deluxe Burger. It's loaded with a juicy beef patty, cheddar cheese, lettuce, tomato, and special sauce.",
                    Price = 7.99
                },
                new Product
                {
                    Url = "#",
                    IconClass = "icon-sushi",
                    Name = "Sushi Platter",
                    ImageUrl = "images/sushi_platter.jpg",
                    CategoryID = 1,
                    Category = categories[1],
                    Title = "Assorted Sushi Platter",
                    Description = "Experience the art of sushi with our Assorted Sushi Platter. It includes a variety of nigiri, maki rolls, and sashimi, crafted with the freshest ingredients.",
                    Price = 12.99
                },
                new Product
                {
                    Url = "#",
                    IconClass = "icon-pasta",
                    Name = "Spaghetti Carbonara",
                    ImageUrl = "images/spaghetti_carbonara.jpg",
                    CategoryID = 1,
                    Category = categories[1],
                    Title = "Creamy Spaghetti Carbonara",
                    Description = "Treat yourself to the creamy goodness of our Spaghetti Carbonara. It's a classic Italian pasta dish made with eggs, cheese, pancetta, and black pepper.",
                    Price = 10.99
                }
        };
        return products.Where(c => query == null ||
            c.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase) ||
            c.Description.Contains(query, StringComparison.InvariantCultureIgnoreCase)).ToList();
    }
}