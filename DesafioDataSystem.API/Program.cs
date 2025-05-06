using DesafioDataSystem.API.Filtros;
using DesafioDataSystem.Application.Interfaces;
using DesafioDataSystem.Application.Services;
using DesafioDataSystem.Infrasturcture;
using DesafioDataSystem.Infrasturcture.Context;
using DesafioDataSystem.Infrasturcture.Migrations;
using DesafioDataSystem.Infrasturcture.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DesafioDataSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            //builder.Services.AddScoped<Application.Interfaces.ITarefaService, TarefaService>();
            builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFiltro)));

            var connectionString = "Data Source=DESKTOP-JVDLEU4;Initial Catalog=gerenciadortarefas;User ID=sa;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";

            builder.Services.AddDbContext<DesafioDataSystemDbContext>(dbContextOptions =>
                dbContextOptions.UseSqlServer(connectionString)
            );



            builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
            builder.Services.AddScoped<ITarefaService, TarefaService>();

            //builder.Services.AddApplication();
            //builder.Services.AddInfrastructure();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                    options.SwaggerEndpoint("/openapi/v1.json", "weather api"));
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(options =>
            {
                options.WithOrigins("http://localhost:59648");
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });

            app.MapControllers();

            MigrateDatabase();

            app.Run();

            void MigrateDatabase()
            {
                DatabaseMigration.Migrate(connectionString);
            }
        }
    }
}
