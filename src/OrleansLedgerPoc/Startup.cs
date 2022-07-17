public class Startup
{
    public void ConfigureServices(IServiceCollection services) =>
        services
        .AddEndpointsApiExplorer()
        .AddSwaggerGen()
        .AddControllers();

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseRouting()
        .UseEndpoints(
                endpoints => endpoints.MapDefaultControllerRoute());

    }
}