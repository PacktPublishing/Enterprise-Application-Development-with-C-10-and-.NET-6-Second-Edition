// See https://aka.ms/new-console-template for more information
using LambdaExpression;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

Console.WriteLine("Hello, World!");

// Defining an expression to calcuate square of a given number
var Square = (int x) => x * x;
Console.WriteLine(Square(10));

var SayHello = (string name) => Console.WriteLine($"Hello {name}");
var SayWelcome = (ref string name) => Console.WriteLine($"Welcome {name}");
String a = "Suneel";
SayWelcome(ref a);

// Expression with return type specified
var createEmployee = Person (bool hasReportees) => hasReportees ? new Manager() : new Employee();
var e = createEmployee(true);

// Expression with argument attributes
var GetEmployeeById =  [Authorize] Employee ([FromRoute]int id) => { return new Employee { }; };

