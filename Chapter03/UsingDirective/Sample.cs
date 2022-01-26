public record Shape(string Name);
public record struct Employee(string FirstName, string LastName);

public struct EmployeeNormalStruct
{
    public EmployeeNormalStruct(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
}
