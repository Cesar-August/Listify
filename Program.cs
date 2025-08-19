using Microsoft.EntityFrameworkCore;
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

// SEED: inserir dados iniciais
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Verifica se já existem usuários (evita duplicação)
    var hasUsers = db.Users.Count() > 0;
    if (!hasUsers)
    {
        var user = new User { Name = "João Silva", Email = "joao@example.com", Password = "senha123" };
        db.Users.Add(user);
        db.SaveChanges();

        var list = new List { Name = "Tarefas da Semana", UserId = user.Id };
        db.Lists.Add(list);
        db.SaveChanges();

        var card = new Card { Title = "Comprar mantimentos", ListId = list.Id };
        db.Cards.Add(card);
        db.SaveChanges();
    }
}

app.Run();