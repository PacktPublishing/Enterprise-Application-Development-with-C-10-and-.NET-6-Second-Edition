// See https://aka.ms/new-console-template for more information


using System;

Console.WriteLine("Hello, World!");

Employee s1 = new Employee("Suneel", "Kunani");
Employee s2 = new Employee("Suneel", "Kunani");
// ToString of record is overwritten to print the properties of the type
Console.WriteLine(s1.ToString());
// GetHashCode of record is overwritten to generate the hash code based on values
Console.WriteLine($"HashCode of s1 is :{ s1.GetHashCode()}");
Console.WriteLine($"HashCode of s2 is :{ s2.GetHashCode()}");
// Equality operator of record type is overwritten to check equality based on the values
Console.WriteLine($"Is s1 equals s2 : { s1 == s2}");

EmployeeNormalStruct s3 = new EmployeeNormalStruct();
s3.FirstName = "Suneel";
s3.LastName = "Kunani";

EmployeeNormalStruct s4 = new EmployeeNormalStruct();
s4.FirstName = "Suneel";
s4.LastName = "Kunani";


// ToString of record is overwritten to print the properties of the type
Console.WriteLine(s3.ToString());
// GetHashCode of record is overwritten to generate the hash code based on values
Console.WriteLine($"HashCode of s3 is :{ s3.GetHashCode()}");
Console.WriteLine($"HashCode of s4 is :{ s4.GetHashCode()}");
// Equality operator of record type is overwritten to check equality based on the values
//Console.WriteLine($"Is s3 equals s4 : { s3 == s4}");