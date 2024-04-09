
using LogisticsManagement.DataAccess.Models;
using LogisticsManagement.DataAccess.Repository;
using LogisticsManagement.DataAccess.Repository.IRepository;
using LogisticsManagement.Service.Convertors;
using LogisticsManagement.Service.Services;
using LogisticsManagement.Service.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace LogisticsManagement.WebAPI
{
    public class Program
    {
        // comment
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<LogisticsManagementContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("localConnectionString")));
            builder.Services.AddAutoMapper(typeof(ApplicationMapper));
            builder.Services.AddScoped<IManagerRepository,ManagerRepository>();
            builder.Services.AddScoped<IManagerService,ManagerService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
