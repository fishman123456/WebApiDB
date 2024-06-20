var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// ����������� 21-06-2024
app.MapGet("/", () => "Hello World!");
app.MapGet("/ping", () => new  { Massage = "pong", Time = DateTime.UtcNow });

// middleware
app.Use(async (context, next) =>
{
    // ������� �� �����������
    Console.WriteLine($"�� ����������� {DateTime.Now}");
    await(next(context));
    // ������� ����� �����������
    Console.WriteLine($"����� ����������� {DateTime.Now}");
});
app.Run();
