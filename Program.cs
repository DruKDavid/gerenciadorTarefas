using System.Globalization;
using Microsoft.EntityFrameworkCore;
using gerenciadorTarefas.Data;

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte a views
builder.Services.AddControllersWithViews();

// Configura o banco de dados MySQL
builder.Services.AddDbContext<BancoContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// Define cultura global para pt-BR
var cultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Constrói o app
var app = builder.Build();

// Pipeline de requisição HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Segurança para ambientes de produção
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Para arquivos estáticos como CSS, JS, imagens etc.
app.UseRouting();
app.UseAuthorization();

// Define a rota padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
