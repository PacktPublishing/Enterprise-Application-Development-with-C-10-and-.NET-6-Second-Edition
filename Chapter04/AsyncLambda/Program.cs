// See https://aka.ms/new-console-template for more information
// Uncomment each sample seperately
using System.Diagnostics;

//// Sample 1
//long elapsedTime = AsyncLambda(async () =>
//{
//    await Task.Delay(1000);
//});

// Sample 2
long elapsedTime = await AsyncLambda(() => Task.Delay(1000));
Console.WriteLine(elapsedTime);
Console.ReadLine();

//// Sample 1
//static long AsyncLambda(Action a)
//{
//    Stopwatch sw = new Stopwatch();
//    sw.Start();
//    for (int i = 0; i < 10; i++)
//    {
//        a();
//    }
//    return sw.ElapsedMilliseconds;
//}

// Sample 2
async static Task<long> AsyncLambda(Func<Task> a)
{
    Stopwatch sw = new Stopwatch();
    sw.Start();
    for (int i = 0; i < 10; i++)
    {
        await a();
    }
    return sw.ElapsedMilliseconds;
}