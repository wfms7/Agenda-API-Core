using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotebook.Data;
using WebNotebook.Models;
using WebNotebook.Services;

namespace WebNotebook
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
            services.AddDbContext<AppDbContext>(opst => opst
            //.UseLazyLoadingProxies()
            .UseMySQL(Configuration.GetConnectionString("PacienteConnection")));
           

            services.AddCors( options => options.AddPolicy("AllowAcess_to_API", policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            ));


            services.AddIdentity<ApplicationUser, IdentityRole<int>>().AddEntityFrameworkStores<AppDbContext>();
            services.AddIdentity<IdentityUser<int>, IdentityRole<int>>(opt => opt.SignIn.RequireConfirmedEmail = false).AddEntityFrameworkStores<AppDbContext>();
            services.AddScoped<RolesService, RolesService>();
            services.AddScoped<UsuarioService, UsuarioService>();
            services.AddScoped<PacienteService, PacienteService>();
            services.AddScoped<TelefoneService, TelefoneService>();
            services.AddScoped<EspecialidadeService, EspecialidadeService>();
            services.AddScoped<AgendaCalendarioService, AgendaCalendarioService>();
            services.AddScoped<Status, Status>();
            services.AddScoped<ConsultaService, ConsultaService>();
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebNotebook", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAcess_to_API");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebNotebook v1"));
            }

            app.UseHttpsRedirection();

         
            app.UseRouting();

            app.UseAuthorization();

            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    internal class CorsAuthorizationFilterFactory : IFilterMetadata
    {
    }
}
