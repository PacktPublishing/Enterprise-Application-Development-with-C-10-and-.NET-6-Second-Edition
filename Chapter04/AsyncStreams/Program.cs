// See https://aka.ms/new-console-template for more information
await foreach (int i in GetEmployeeIDAsync(5))
{
    Console.WriteLine(i);
}
Console.ReadLine();

static async IAsyncEnumerable<int> GetEmployeeIDAsync(int input)
{
    int id = 0;
    List<int> tempID = new List<int>();
    for (int i = 0; i < input; i++)
    {
        await Task.Delay(1000);
        id += i; // Hypothetical calculation
        yield return id;
    }
}