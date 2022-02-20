// See https://aka.ms/new-console-template for more information
using static System.Console;
using RecordStruct;


Employee employee1 = new Employee("Suneel", "Kunani");
Employee employee2 = new Employee("Suneel", "Kunani");
// ToString of record struct is overwritten to print the properties of the type
WriteLine(employee1.ToString());
// GetHashCode of record struct is overwritten to generate the hash code based on values
WriteLine($"HashCode of s1 is :{ employee1.GetHashCode()}");
WriteLine($"HashCode of s2 is :{ employee2.GetHashCode()}");
// Equality operator of record type is overwritten to check equality based on the values
WriteLine($"Is s1 equals s2 : { employee1 == employee2}");
//deconstruct the fields from the emplyee object
string firstname;
(firstname, var lastname) = employee1;
// The previous line has mix declaration of varaible, 
// we are declaring lastName during deconstruction where as firstName is declared before deconstruction.
// This is possible only in C# 10 and above.
// IN C# 9 we will do something like this
// var (firstname, lastname) = employee1;

Console.WriteLine($"firstname: {firstname}, lastname:{lastname}");
