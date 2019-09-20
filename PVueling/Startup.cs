using System;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using PVueling.Application;
using PVueling.Domain.Interface;
using PVueling.Infraestruct.ApiService;
using PVueling.Infraestruct.Data;
using PVueling.Infraestruct.Repository;

namespace PVueling
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
            services.AddHttpClient("httpRates", client =>
            {
                client.BaseAddress = new Uri("http://quiet-stone-2094.herokuapp.com/rates.json");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent","application/json");
            });
            services.AddHttpClient("httpTransactions", client =>
            {
                client.BaseAddress = new Uri("http://quiet-stone-2094.herokuapp.com/transactions.json");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", "application/json");
            });


            var retryPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10));
            var retryPolicyR = HttpPolicyExtensions.HandleTransientHttpError().RetryAsync(3);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<IServiceApiRate, ServiceApiRate>();
            services.AddScoped<IServiceApiTransaction, ServiceApiTransaction>();
            services.AddScoped<IDataService, DataService>();
            services.AddScoped<IFind, DataService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IRepository, Repository>();
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddEntityFrameworkSqlite().AddDbContext<Infraestruct.RepositoryDB.MyDbContextRateTransac>();
            services.AddHttpClient("httpRates").AddPolicyHandler(retryPolicy);
            services.AddHttpClient(" httpRates ")
            .AddTransientHttpErrorPolicy(p => p.RetryAsync(3));
            services.AddHttpClient("httpTransaction").AddPolicyHandler(retryPolicy);
            services.AddHttpClient(" httpTransaction ")
            .AddTransientHttpErrorPolicy(p => p.RetryAsync(3));
            var noOp = Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>();
            services.AddHttpClient("httpRates").AddPolicyHandler(request => request.Method == HttpMethod.Get ? retryPolicy : noOp);
            services.AddHttpClient("httpTransaction").AddPolicyHandler(request => request.Method == HttpMethod.Get ? retryPolicy : noOp);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            using (var servicedatabase=app.ApplicationServices.CreateScope())
            {
                var context = servicedatabase.ServiceProvider.GetService<Infraestruct.RepositoryDB.MyDbContextRateTransac>();
                var fileDb = "TestDatabase.db";
                if (!File.Exists(fileDb))
                {
                    context.Database.EnsureCreated();
                }
            }

           
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddFile("Log-MyApp.txt");
          
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "text/html";

                    await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                    await context.Response.WriteAsync("ERROR!<br><br>\r\n");

                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    // Use exceptionHandlerPathFeature to process the exception (for example, 
                    // logging), but do NOT expose sensitive error information directly to 
                    // the client.

                    if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                    {
                        await context.Response.WriteAsync("No hemos encontrado tu ruta!<br><br> Las rutas que funcionan en esta API son <br><br>" +
                             " /Rates -> Devuelve todas las conversiones <br><br>" +
                            " /Transanctions -> Devuelve todas las transacciones <br><br>" +
                            "/Transanctions/codigosku -> Devuelve todas las transacciones con en las que aparezca ese sku \r\n");
                    }

                    await context.Response.WriteAsync("<a href=\"/\">Home</a><br>\r\n");
                    await context.Response.WriteAsync("</body></html>\r\n");
                    await context.Response.WriteAsync(new string(' ', 512)); // IE padding
                });
            });
           
        }
                       
        app.UseMvc();
        }
    }
}
