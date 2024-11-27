using PokemonTcg.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 添加服務註冊
builder.Services.AddScoped<ICardService, CardService>();

// CORS 配置
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp",
        builder =>
        {
            builder
                .SetIsOriginAllowed(origin => true) // 允許任何來源
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 配置 Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Pokemon TCG API", 
        Version = "v1",
        Description = "A simple Pokemon Trading Card Game API"
    });
});

// 配置 Kestrel
builder.WebHost.UseUrls("http://localhost:5001");

var app = builder.Build();

// 重要：將 CORS 中間件放在其他中間件之前
app.UseCors("AllowVueApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
