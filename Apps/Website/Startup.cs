using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HN.Application;
using HN.Domain;
using HN.Infrastructure;
using HN.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Website
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      //var connstr = Configuration.GetSection("ConnectionStrings")["Default"];
      services.AddDbContext<HNDbContext>(options =>
        options.UseSqlite(Configuration.GetConnectionString("Default")));
      // - Scoped : durée de vie = durée de la requête
      services.AddScoped<ILinkRepository, LinkRepository>();
      services.AddScoped<IHNContext, HNDbContext>();
      services.AddMediatR(typeof(HN.Application.PublishLinkCommand));

      // - Singleton : durée de vie = durée de vie de l'application
      // services.AddSingleton
      // - Transient : à chaque nouvelle demande = nouvelle instance retournée
      // services.AddTransient

      services.AddControllersWithViews();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      MigrateDatabase(app);

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      // Exemple de middleware permettant de "catcher" toutes les exceptions survenants
      // plus loin dans le pipeline
      app.Use(async (ctx, next) =>
      {
        // ctx.RequestServices
        //   var auth = ctx.Request.Headers["Authorization"];
        // timer start
        try
        {
          await next();
        }
        catch (Exception e)
        {
          throw e;
        }
        // timer end
      });

      // L'ordre des appels compte ici ...
      app.Use(async (ctx, next) =>
      {
        await next();
      });

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
      });
    }

    public void MigrateDatabase(IApplicationBuilder app)
    {
      using var scope = app.ApplicationServices.CreateScope();
      using var context = scope.ServiceProvider.GetRequiredService<HNDbContext>();
      context.Database.Migrate();

      // using (var scope = app.ApplicationServices.CreateScope())
      // {
      //   using (var context = scope.ServiceProvider.GetRequiredService<HNDbContext>())
      //   {
      //     context.Database.Migrate();
      //   }
      // }
    }
  }
}
