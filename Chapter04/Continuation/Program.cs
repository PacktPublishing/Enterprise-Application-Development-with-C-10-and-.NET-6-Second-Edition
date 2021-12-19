// See https://aka.ms/new-console-template for more information
Task.Factory.StartNew(() => Task1(1)) // 1+2 = 3
                .ContinueWith(a => Task2(a.Result)) // 3*2 = 6
                    .ContinueWith(b => Task3(b.Result))// 6-2=4
                        .ContinueWith(c => Console.WriteLine(c.Result));
Console.ReadLine();

static int Task1(int a) => a + 2;
static int Task2(int a) => a * 2;
static int Task3(int a) => a - 2;