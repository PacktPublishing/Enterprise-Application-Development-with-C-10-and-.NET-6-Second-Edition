// See https://aka.ms/new-console-template for more information
Thread loadFileFromDisk = new Thread(LoadFileFromDisk);

void LoadFileFromDisk(object? obj)
{
    Thread.Sleep(2000);
    Console.WriteLine("data returned from API");
}

loadFileFromDisk.Start();
Thread fetchDataFromAPI = new Thread(FetchDataFromAPI);

void FetchDataFromAPI(object? obj)
{
    Thread.Sleep(2000);
    Console.WriteLine("File loaded from disk");
}

fetchDataFromAPI.Start("https://dummy/v1/api"); //Parameterized method
Console.ReadLine();
