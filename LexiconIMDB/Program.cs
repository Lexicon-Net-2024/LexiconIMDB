using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LexiconIMDB.Data;
using LexiconIMDB.Services;
namespace LexiconIMDB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<LexiconIMDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("LexiconIMDBContext") ?? throw new InvalidOperationException("Connection string 'LexiconIMDBContext' not found.")));

            builder.Services.AddScoped<IGenreSelectListService, GenreSelectListService>();

            //builder.Services.AddSingleton -> Samma instans f�r hela applikationens livsl�ngd
            //builder.Services.AddScoped ->  Samma instans f�r hela v�rt request
            //builder.Services.Transient -> Ny instans s� fort n�gon beh�ver servicen. 

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Movie/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Movies}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
