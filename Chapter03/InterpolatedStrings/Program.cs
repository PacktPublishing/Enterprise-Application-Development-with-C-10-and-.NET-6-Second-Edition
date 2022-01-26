// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string Greeting = "Hello";
string Language = "C#";
int version = 10;
string message = $"{Greeting}, {Language}!";
string messageWithVersion = $"{Greeting}, {Language} {version}!";

Span<char> test = new Span<char>();
ReadOnlySpan<char> a;
string messageX = $"{Greeting}, {Language} {test}!";
