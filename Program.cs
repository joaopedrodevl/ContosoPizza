Console.WriteLine("Hello World!");

// Product veggieSpecial = new()
// {
//     Name = "Veggie Special Pizza",
//     Price = 9.99M
// };
// context.Products.Add(veggieSpecial); // Add is a shorthand method for adding an entity to the context.

// Product deluxeMeat = new()
// {
//     Name = "Deluxe Meat Pizza",
//     Price = 12.99M
// };
// context.Add(deluxeMeat); // Add is a shorthand method for adding an entity to the context.

// context.SaveChanges(); // SaveChanges persists the changes to the database.

// Fluent API with LINQ
// var products = context.Products
//     .Where(p => p.Price > 10.00M)
//     .OrderBy(p => p.Name);

// foreach (var product in products)
// {
//     Console.WriteLine($"Name: {product.Name} Price: {product.Price}");
//     Console.WriteLine(new string('-', 20));
// }

// var veggieSpecial = context.Products
//                             .Where(p => p.Name == "Veggie Special Pizza")
//                             .FirstOrDefault(); // FirstOrDefault returns the first element of a sequence, or a default value if no element is found. 

// if (veggieSpecial is not null)
// {
//     // veggieSpecial.Price = 10.99M; // Update the price of the Veggie Special Pizza
//     context.Remove(veggieSpecial); // Remove the Veggie Special Pizza
// }

// context.SaveChanges();

// var products = from product in context.Products // LINQ query syntax
//                where product.Price > 10.00M
//                orderby product.Name
//                select product;

// foreach (var product in products)
// {
//     Console.WriteLine($"Name: {product.Name} Price: {product.Price}");
//     Console.WriteLine(new string('-', 20));
// }