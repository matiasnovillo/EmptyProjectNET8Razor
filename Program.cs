using FiyiStore.Areas.BasicCore.Repositories;
using FiyiStore.Areas.CMSCore.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//Set access to repositories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<MenuRepository>();
builder.Services.AddScoped<RoleMenuRepository>();
builder.Services.AddScoped<FailureRepository>();
builder.Services.AddScoped<ParameterRepository>();

//Set access to repositories from FiyiStackWeb2

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
