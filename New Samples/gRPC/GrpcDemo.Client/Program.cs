using Grpc.Net.Client;
using GrpcDemo.Server;

Console.WriteLine("=== gRPC Demo Client ===");
Console.WriteLine();

// Connect to the server over plain HTTP (no SSL setup needed for the demo)
using var channel = GrpcChannel.ForAddress("http://localhost:5144");

// The client stub is auto-generated at build time from greet.proto by Grpc.Tools
var client = new Greeter.GreeterClient(channel);

// --- Unary RPC: one request → one response ---
Console.WriteLine("Sending SayHello(\"World\")...");
var reply = await client.SayHelloAsync(new HelloRequest { Name = "World" });
Console.WriteLine($"Server replied: {reply.Message}");

Console.WriteLine();

// Send a few more calls to show it working
string[] names = ["Alice", "Bob", "Charlie"];
foreach (var name in names)
{
    var r = await client.SayHelloAsync(new HelloRequest { Name = name });
    Console.WriteLine($"  → {r.Message}");
}

Console.WriteLine();
Console.WriteLine("Done. Press any key to exit...");
Console.ReadKey();
