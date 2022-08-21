using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasosUsos;
using Dominio.InterfacesRepositorio;
using Dominio.EntidadesNegocio;
using Repositorios;

namespace WebObligatoriop3
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddSession(options => {

                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
                  
            services.AddScoped<IManejadorUsuario, ManejadorUsuario>();
            services.AddScoped<IRepositorioUsuario, RepositorioUsuarioADO>();
            services.AddScoped<IManejadorPlantas, ManejadorPlantas>();
            services.AddScoped<IRepositorioTipoDePlanta, RepositorioTipoDePlantaADO>();
            services.AddScoped<IRepositorioPlanta, RepositorioPlantaADO>();
            services.AddScoped<IRepositorio<TipoDeIluminacion>, RepositorioTipoDeIluminacionADO>();
            services.AddScoped<IRepositorio<FichaDeCuidado>, RepositorioFichaDeCiudadoADO>();
            services.AddScoped<IRepositorio<Compra>, RepositorioCompraADO>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=login}/{id?}");
            });
        }
    }
}
