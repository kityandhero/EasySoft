// See https://aka.ms/new-console-template for more information

using EntranceService;
using Grpc.Net.Client;

Console.WriteLine("Hello, World!");

using var channel = GrpcChannel.ForAddress("https://localhost:8865");

var client = new Entrance.EntranceClient(channel);
var reply = await client.RegisterAsync(
    new RegisterRequest
    {
        LoginName = "GreeterClient"
    });
Console.WriteLine("Greeting: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();