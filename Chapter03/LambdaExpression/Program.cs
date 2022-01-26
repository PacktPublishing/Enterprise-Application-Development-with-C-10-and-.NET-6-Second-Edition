// See https://aka.ms/new-console-template for more information
using LambdaExpression;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

Console.WriteLine("Hello, World!");


var Square = (int x) => x * x;
Console.WriteLine(Square(10));

var SayHello = (string name) => Console.WriteLine($"Hello {name}");
var SayWelcome = (ref string name) => Console.WriteLine($"Welcome {name}");
String a = "asdfasdf";
SayWelcome(ref a);

var somemethod = Person (bool condition) => condition ? new Employee() : new Manager();

var GetEmployeeById =  [Authorize] Employee ([FromRoute]int id) => { return new Employee { }; };

