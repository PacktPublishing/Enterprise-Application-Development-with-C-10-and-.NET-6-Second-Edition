// See https://aka.ms/new-console-template for more information


using ExtendedProperty;

Product product = new Product
{
    Name = "Men's Shirt",
    Price = 10.0m,
    Location = new Address
    {
        Country = "USA",
        State = "NY"
    }
};

// Accessing the extended propers 
if (product is Product { Location: { Country: "USA" } })
    Console.WriteLine("USA");

// Accessing the extended properties with additional support in C#10
if (product is Product { Location.Country: "USA" })
    Console.WriteLine("USA");
