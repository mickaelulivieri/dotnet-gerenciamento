using Microsoft.EntityFrameworkCore;
using TaskManager.API.Data;
using TaskManager.API.Mappings;
using TaskManager.API.Repositories;
using TaskManager.API.Services;
using TaskManager.API.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// 1. CORS - Prioridade máxima para liberar o React
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => 
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// 2. Banco de Dados com estratégia de retentativa
var connectionString = "server=db;port=3306;database=taskmanager;user=root;password=senha";
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, 
        new MySqlServerVersion(new Version(8, 0, 36)),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(5), null))
);

// 3. Injeção de Dependência
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// 4. Configuração do Pipeline (A ordem aqui é o segredo)
app.UseRouting();
app.UseCors("AllowAll"); // Tem que vir antes de Authorization e Middleware

app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();
app.MapControllers();

// 5. Migração em segundo plano para não travar a API no início
_ = Task.Run(async () => {
    try {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await Task.Delay(10000); // Espera o MySQL estabilizar
        await context.Database.MigrateAsync();
        Console.WriteLine("✅ Banco de dados sincronizado com sucesso!");
    } catch (Exception ex) {
        Console.WriteLine($"❌ Erro na sincronização: {ex.Message}");
    }
});

app.Run();