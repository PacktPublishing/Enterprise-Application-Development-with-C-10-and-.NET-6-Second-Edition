// See https://aka.ms/new-console-template for more information
// Uncomment each sample seperately
// Sample 1
Task dataTask = new Task(() => FetchDataFromAPI("https://www.microsoft.com/en-in/"));

dataTask.Start();

// Sample 2
//Task dataTask = Task.Run(() => FetchDataFromAPI("https://www.microsoft.com/en-in/"));

Task t = Task.Factory.StartNew(() => FetchDataFromAPI("https://www.microsoft.com/en-in/"));
t.Wait();

Console.ReadLine();

void FetchDataFromAPI(string v)
{
    Thread.Sleep(2000);
    Console.WriteLine("data returned from API");
}