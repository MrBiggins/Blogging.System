using Blogging.System.Business.Logic;
using Blogging.System.Infrastructure;
using Blogging.System.Infrastructure.Extentions;
using Blogging.System.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddBusinessLogic();
builder.Services.AddInfrastructure();

builder.Services.AddOpenApi();
//builder.Services
//    .AddControllers(o => o.ReturnHttpNotAcceptable = true)
//    .AddXmlSerializerFormatters();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new() { Title = "Blogging.System.WebApi", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blogging.System.WebApi v1"));
    app.Services.SeedBlogData();

}

app.UseExceptionHandlingMiddleware();   

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
