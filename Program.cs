using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build(); 

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ) //If you will be add logging then think of adding the ex and using it 
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("Something went wrong! ");
        
    }
});
}

// app.UseAuthentication();
// app.UseAuthorization();

app.UseRouting();
app.MapControllers();
app.Run();
