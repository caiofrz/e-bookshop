namespace frontend.Application.Configurations;

public static class WebAppConfig
{
    public static IApplicationBuilder ConfigureApp(this IApplicationBuilder app,
                                                        IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
        });

        return app;
    }
}