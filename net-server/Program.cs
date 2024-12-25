using Microsoft.EntityFrameworkCore;
using net_server.Models;

var builder = WebApplication.CreateBuilder(args);

#region 配置数据库

// Add services to the container.
var config = new ConfigurationBuilder()
           .AddInMemoryCollection() //将配置文件的数据加载到内存中
           .SetBasePath(Directory.GetCurrentDirectory()) //指定配置文件所在的目录
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) //指定加载的配置文件  --划重点..记得始终复制
           .Build(); //编译成对象  

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<GegeDbContext>(optionsBuilder => optionsBuilder.UseMySql(config.GetConnectionString("ConnectionString"), new MySqlServerVersion(new Version(8, 0, 34)), mySqlOptions =>
{
    mySqlOptions.MigrationsAssembly("net-server.API");
}
), poolSize: 5);

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
