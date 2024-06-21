using Microsoft.AspNetCore.Http.Extensions;
using WebApiDB.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>();
var app = builder.Build();

// ����������� 21-06-2024
app.MapGet("/", () => "Hello World!");
app.MapGet("/ping", () => new  { Massage = "pong", Time = DateTime.UtcNow });

// middleware
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    DateTime receiveTime = DateTime.UtcNow;
    // ������� ������ �� iok ����������
    ApplicationDbContext db = context.RequestServices.GetService<ApplicationDbContext>();
    // ������� �� �����������
    Console.WriteLine($"�� ����������� {DateTime.Now}");

    await(next(context));
    // ������� ����� �����������
    ReqestData reqestData = new ReqestData()
    {
        ReceiveTime = receiveTime,
        CompleteTime = DateTime.UtcNow,
        HttpMethod = context.Request.Method,
        StatusCode = context.Response.StatusCode,
        URI = context.Request.GetEncodedUrl()
    };
    await db.ReqestDatas.AddAsync(reqestData);
    await db.SaveChangesAsync();
    Console.WriteLine($"����� ����������� {DateTime.Now}");
});
app.Run();
