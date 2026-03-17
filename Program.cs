using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using TodoListApp;
using TodoListApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Carregar configurações do arquivo appsettings.json
var configuration = builder.Configuration;

// Configurar o DbContext para o Oracle
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseOracle(
        builder.Configuration.GetConnectionString("OracleConnection"),
        oracleOptions =>
        {
            oracleOptions.UseRelationalNulls(false); // Evita TRUE/FALSE
        }
    );
});
// Adicionar MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Pipeline de requisições HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Rotas
app.MapControllerRoute(
    name: "dashboard",
    pattern: "dashboard",
    defaults: new { controller = "Dashboard", action = "IndexDash" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// SEED: Inserir dados iniciais (opcional)
// ...

// SEED: inserir dados iniciais (assíncrono) com retry/backoff para aguardar o DB
await using (var scope = app.Services.CreateAsyncScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var maxAttempts = 6;
    var delay = TimeSpan.FromSeconds(2);
    var connected = false;

    for (int attempt = 1; attempt <= maxAttempts; attempt++)
    {
        try
        {
            if (await db.Database.CanConnectAsync())
            {
                connected = true;
                break;
            }

            logger.LogWarning("Tentativa {Attempt}/{MaxAttempts}: banco não disponível ainda. Aguardando {Delay}...", attempt, maxAttempts, delay);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Tentativa {Attempt}/{MaxAttempts}: falha ao conectar ao banco. Aguardando {Delay}...", attempt, maxAttempts, delay);
        }

        await Task.Delay(delay);
        delay = delay * 2; // backoff exponencial
    }

    if (!connected)
    {
        logger.LogError("Não foi possível conectar ao banco após {MaxAttempts} tentativas. Seed abortado.", maxAttempts);
    }
    else
    {
        // Verifica se já existem usuários (evita duplicação) — AnyAsync() é mais eficiente que Count() > 0
        var hasUsers = await db.Users.AnyAsync();
        if (!hasUsers)
        {
            var user = new User { Name = "João Silva", Email = "joao@example.com", Password = "senha123" };
            db.Users.Add(user);
            await db.SaveChangesAsync();

            var list = new List { Name = "Tarefas da Semana", UserId = user.Id };
            db.Lists.Add(list);
            await db.SaveChangesAsync();

            var card = new Card { Title = "Comprar mantimentos", ListId = list.Id };
            db.Cards.Add(card);
            await db.SaveChangesAsync();
        }
    }
}

app.Run();