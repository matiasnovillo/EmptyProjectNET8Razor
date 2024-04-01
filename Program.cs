using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using FiyiStore.Areas.BasicCore;
using FiyiStore.Areas.BasicCore.Entities;
using FiyiStore.Areas.BasicCore.Interfaces;
using FiyiStore.Areas.BasicCore.Repositories;
using FiyiStore.Areas.CMSCore.Interfaces;
using FiyiStore.Areas.CMSCore.Repositories;
using FiyiStore.Areas.CMSCore.Services;
using FiyiStore.Library;

var builder = WebApplication.CreateBuilder(args);

//Use session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//JSON to TimeSpan configuration
builder.Services.AddControllers()
.AddJsonOptions(options =>
options.JsonSerializerOptions.Converters.Add(new JsonToTimeSpan()));

//JSON configuration to output field names in PascalCase. Example: "TestId" : 1 and not "testId" : 1
builder.Services.AddControllers()
.AddJsonOptions(options =>
options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddDbContext<FiyiStoreContext>(ServiceLifetime.Scoped);

//Set access to repositories
builder.Services.AddScoped<IFailureRepository, FailureRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleMenuRepository, RoleMenuRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IParameterRepository, ParameterRepository>();

//Set access to services
builder.Services.AddScoped<IUserService, UserService>();

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
app.UseSession();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapDefaultControllerRoute();
});
app.Run();
