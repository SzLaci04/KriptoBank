using KriptoBank.Services;
using KriptoBank.DataContext.Context;
using KriptoBank.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//Server=(local);Database=CryptoDb_NEPTUN;Trusted_Connection=True;TrustServerCertificate=True;
string connectionString = "Server=(LocalDB)\\MSSQLLocalDB;Database=CryptoDb_M5ZCE4;Trusted_Connection=True;TrustServerCertificate=\r\nTrue;";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString, options => options.MigrationsAssembly("KriptoBank"));
}
);

builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IWalletServices, WalletServices>();
builder.Services.AddScoped<ICryptoServices, CryptoServices>();
builder.Services.AddScoped<ITradeServices, TradeServices>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "KriptoBank API",
        Version = "v1",
        Description = "Egy oktatási kriptovaluta kereskedõ alkalmazás API",
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "KriptoBank API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
