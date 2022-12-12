using AppLivrosDesafio.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using AppLivrosDesafio.Data.Models;
using AppLivrosDesafio.Data.Interfaces;
using AppLivrosDesafio.Data.Repository;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// utilizei:
// Entity Framework para migrations
// Identity para tela de login 
// MVC para as funcionalidades da tela 
// Qualquer dúvida sobre como tudo foi usado estou disposto a responder. Obrigado.

builder.Services.AddDbContext<AppLivrosDesafioContext>(options =>
    options.UseMySql(
    builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 1))));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//Adicionando extensão do mvc core pra razor pages. - 08/12 - YAGO
builder.Services.AddRazorPages();


builder.Services.AddDefaultIdentity<AppLivrosUser>(
    
    options => options.SignIn.RequireConfirmedAccount = false)
    
    .AddEntityFrameworkStores<AppLivrosDesafioContext>();


//ADD TRANSIENT, TODA VEZ QUE O CONTROLADOR FOR INSTANCIADO, SERÁ GERADA UMA NOVA INSTÂNCIA DO OBJETO - 08/12 - YAGO
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

builder.Services.AddControllersWithViews();

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    
    app.UseHsts();
}

// Funcionalidade para salvar imagems dos livros - 09/12 - YAGO
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
                        RequestPath="/Images"
});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=UserDetail}/{id?}");
app.MapRazorPages();

app.Run();
