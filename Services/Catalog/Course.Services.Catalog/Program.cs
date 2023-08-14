using Course.Services.Catalog.Services;
using Course.Services.Catalog.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddControllers();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers(option =>
{
	option.Filters.Add(new AuthorizeFilter());
});

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
	return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Course.Services.Catalog", Version = "v1" });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
	option.Authority = builder.Configuration["IdentityServerURL"];
	option.Audience = "resource_catalog";
	option.RequireHttpsMetadata = false;
});

var app = builder.Build();
app.UseRouting();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
	//app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Course.Services.Catalog v1"));
}

app.MapControllers();
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});

app.Run();