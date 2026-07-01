using _exception_middleware;
using _auth_service;
using _budget_service;
using _transfer_service;
using _wallet_service;
using _interfaces;
using _context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace MultiWallet_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IWalletService, WalletService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITransferService, TransferService>();
            builder.Services.AddScoped<IBudgetService, BudgetService>();

            // логгер
            builder.Logging.ClearProviders(); 
            builder.Logging.AddConsole();     


            var connectionString = builder.Configuration.GetConnectionString("PostgresDB");

            builder.Services.AddDbContext<AppDbContext>(x =>
            {
                x.UseNpgsql(connectionString);
            });

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();
            builder.Services.AddControllers();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            
            // middleware
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            // middleware

            app.MapControllers();

            app.Run();
        }
    }
}
