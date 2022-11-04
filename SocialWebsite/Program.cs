using Microsoft.EntityFrameworkCore;
using SocialWebsite.Data;
using SocialWebsite.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// set default page
builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
{
    options.Conventions.AddPageRoute("/Posts/Index", "");
});
// Add Db services
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
// Add auto mapper services
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Add signalR service
builder.Services.AddSignalR();
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

// for status code error
app.UseStatusCodePagesWithRedirects("/ErrorPages?statusCode={0}");

app.UseAuthorization();

app.MapRazorPages();

app.MapHub<PostHub>("/hubs/post");

app.Run();
