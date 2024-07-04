using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using WebApiDB.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>();
var app = builder.Build();

// обработчики 21-06-2024
app.MapGet(
    "/", () => "Hello World!");
app.MapGet(
    "/ping", () => new  { Massage = "pong", Time = DateTime.UtcNow });
app.MapGet(
    "/reqests", async (ApplicationDbContext db) => await db.ReqestDatas.ToListAsync());
app.MapGet(
    "/reqests/{id:int}", async (ApplicationDbContext db, int id) => await db.ReqestDatas.FirstAsync(r => r.id == id));
// строка соединения  до изменения 04-07-2024
// "CloudDbConnection": "Data Source= sql.bsite.net\\MSSQL2016 ; Initial Catalog = denf_ ;  User ID = denf_; Password = Fishman_11; TrustServerCertificate = true"
// "Data Source=FISHMAN; Initial Catalog=pv225_requests_db; Integrated Security=SSPI; Timeout=5; TrustServerCertificate=true",
// middleware
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    DateTime receiveTime = DateTime.UtcNow;
    // достать сервис из iok контейнера
    ApplicationDbContext db = context.RequestServices.GetService<ApplicationDbContext>();
    // дествие до обработчика
    Console.WriteLine($"до обработчика {DateTime.Now}");

    await(next(context));
    // дествие после обработчика
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
    Console.WriteLine($"после обработчика {DateTime.Now}");
});
app.Run();
