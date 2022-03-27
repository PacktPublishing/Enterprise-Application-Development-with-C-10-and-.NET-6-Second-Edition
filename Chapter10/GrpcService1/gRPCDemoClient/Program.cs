// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using GrpcService1;

var channel = GrpcChannel.ForAddress("https://localhost:5001");
var client = new Greeter.GreeterClient(channel);
HelloReply response = await client.SayHelloAsync(new HelloRequest { Name = "Suneel" });
Console.WriteLine(response.Message);

