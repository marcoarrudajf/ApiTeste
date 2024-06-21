using ApiTeste.Services;
using ApiTeste.Repositories;
using ApiTeste.Repositories.Interface;
using ApiTeste.Services.Interface;
using Microsoft.EntityFrameworkCore;
using ApiTeste.Domain;

namespace ApiTeste
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            // Add services to the container.
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            
            var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");

            // Add DbContext with MySQL
            builder.Services.AddDbContext<MeuDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            );

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            //Teste de conexão
            app.MapGet("/test-connection", async (MeuDbContext context) =>
            {
                try
                {
                    await context.Database.OpenConnectionAsync();
                    await context.Database.CloseConnectionAsync();
                    return Results.Ok("Conexão bem-sucedida!");
                }
                catch (Exception ex)
                {
                    return Results.Problem("Erro ao conectar: " + ex.Message);
                }
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
