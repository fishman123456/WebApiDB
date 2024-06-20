var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// обработчики 21-06-2024
app.MapGet("/", () => "Hello World!");
app.MapGet("/ping", () => new  { Massage = "pong", Time = DateTime.UtcNow });

// middleware
app.Use(async (context, next) =>
{
    // дествие до обработчика
    Console.WriteLine($"до обработчика {DateTime.Now}");
    await(next(context));
    // дествие после обработчика
    Console.WriteLine($"после обработчика {DateTime.Now}");
});
app.Run();
