using CursoProgressao.WebClient;
using CursoProgressao.WebClient.Repositories.Students;
using CursoProgressao.WebClient.Services.Students;
using CursoProgressao.WebClient.Stores;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddTransient<IStudentsRepository, FakeStudentsRepository>();
builder.Services.AddTransient<IStudentsService, StudentsService>();
builder.Services.AddSingleton<StudentsStore>();

await builder.Build().RunAsync();
