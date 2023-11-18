// Startup.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Hosting;


public class Startup
{
    private readonly IWebHostEnvironment _environment;
    public Startup(IConfiguration configuration,IWebHostEnvironment environment)
    {
        Configuration = configuration;
        _environment = environment;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IProductService, ProductService>();
        services.AddControllers();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerceApi", Version = "v1" });
        });

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
       
        app.UseRouting();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerceApi V1");
        });
        app.UseHttpsRedirection();
        //app.Run();
    }
}
