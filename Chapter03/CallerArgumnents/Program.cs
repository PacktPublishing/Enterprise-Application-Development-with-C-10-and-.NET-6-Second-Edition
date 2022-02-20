// See https://aka.ms/new-console-template for more information
using CallerArgumnents;
using System.Drawing;

Console.WriteLine("Hello, World!");
Gift employee = new Gift();
//(employee == null).IsTrue();
Product p = null;
//The following call to AddToCart will throw an error as the product passed in is null
// The validation is done by the Helper class ArgumentValidation
AddToCart(p, 1);

static void AddToCart(Product product, uint quantity)
{
    ArgumentValidation.ThrowIfNull(product);
    // Implementation to add the product to cart
}