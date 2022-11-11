// See https://aka.ms/new-console-template for more information

using System;
using EntranceService;
using Grpc.Net.Client;

Console.WriteLine("Hello, World!");

using var channel = GrpcChannel.ForAddress(
    "https://localhost:8866"
);

var client = new Entrance.EntranceClient(channel);
var reply = await client.RegisterAsync(
    new RegisterRequest
    {
        LoginName = "GreeterClient"
    });
Console.WriteLine("Message: " + reply.Message);
Console.WriteLine("Press any key to exit...");

// var color = ConsoleColor.Green;

Console.ReadKey();