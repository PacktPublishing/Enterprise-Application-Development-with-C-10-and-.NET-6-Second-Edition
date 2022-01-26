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

