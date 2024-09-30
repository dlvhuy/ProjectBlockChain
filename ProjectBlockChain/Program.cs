
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectBlockChain.Models;
using ProjectBlockChain.Repositories;
using System.Text;

namespace ProjectBlockChain
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.

      builder.Services.AddControllers();
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      builder.Services.AddDbContextPool<BankingContext>(option =>
      {
        option.UseSqlServer(builder.Configuration.GetConnectionString("BankingDbConnect"));
      });

      var SecretKeyBytes = Encoding.UTF8.GetBytes(builder.Configuration["AppSetting:SecretKey"]);

      builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
      {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateAudience = false,
          ValidateIssuer = false,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(SecretKeyBytes),
          ClockSkew = TimeSpan.Zero
        };
      });

      builder.Services.AddScoped<IUserRepository,UserRepository>();
      builder.Services.AddScoped<ITransactionRepository,TransactionRepository>();

      var app = builder.Build();

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
