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

if (product is Product { Location: { Country: "USA" } })
    Console.WriteLine("USA");

if (product is Product { Location.Country: "USA" })
    Console.WriteLine("USA");
