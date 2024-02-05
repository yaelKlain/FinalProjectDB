using DALL.Data;
using DALL.Interfase;
using Microsoft.EntityFrameworkCore;
using Modells;
using ProjectDb1.Middlware;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        
        string Cors = "_Cors";
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<IToDo, ToDoData>();
        builder.Services.AddScoped<Ipost, PostData>();
        builder.Services.AddCors(op =>
        {
            op.AddPolicy(Cors, builder =>
            {
                builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
            });
        });
        builder.Services.AddDbContext<ProjectContext>(op => op.UseSqlServer("Data Source=DESKTOP-L1QQEC3\\SQLEXPRESS;Initial Catalog=Dproject1;Integrated Security=SSPI;Trusted_Connection=True;"));


        Log.Logger = new LoggerConfiguration().WriteTo.File(@"C:\Users\1\Desktop\תכנות\שנה ב\ProjNow\ProjectDb1.txt", rollingInterval:RollingInterval.Day)
            .CreateLogger();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseMiddleware<ErrorGlobalMiddleware>();
        app.UseMiddleware<Middlware>();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

            

        app.UseCors(Cors);

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}