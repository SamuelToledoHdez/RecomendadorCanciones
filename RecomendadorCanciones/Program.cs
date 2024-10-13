using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using RecomendadorCanciones.Data;

namespace RecomendadorCanciones
{
    public class Program
    {
        public static void Main(string[] args)
        {

           
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlConnection")));

            // Add Swagger
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();  // Añadir este servicio para que Swagger detecte los controladores


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();  // Asegúrate de mapear los controladores
            });

            // Enable Swagger middleware
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                c.RoutePrefix = string.Empty;  // Esto sirve Swagger en la ruta base
            });

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
