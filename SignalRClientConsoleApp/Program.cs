using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("Hello, World!");

Console.WriteLine("SignalR Console Client");

var connection = new HubConnectionBuilder().WithUrl("https://localhost:7190/exampleTypeSafeHub").Build();

connection.StartAsync().ContinueWith((result) =>
{
    Console.WriteLine(result.IsCompletedSuccessfully ? "Connected" : "Connection failed");
});

connection.On<Product>("ReceiveTypedMessageForAllClient",
    (product) => { Console.WriteLine($"Received message: {product.Id}-{product.Name}-{product.Price}"); });


while (true)
{
    var key = Console.ReadLine();

    if (key == "exit") break;

    var newProduct = new Product(200, "pen 200", 250);

    await connection.InvokeAsync("BroadcastTypedMessageToAllClient", newProduct);
}