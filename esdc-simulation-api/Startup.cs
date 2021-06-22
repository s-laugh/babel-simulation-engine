using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using RestSharp;

using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Storage;
using esdc_simulation_base.Src.Rules;

using sample_scenario;
using sample_scenario.Rules;

using maternity_benefits;
using maternity_benefits.Rules;
using maternity_benefits.Storage.EF;
using maternity_benefits.Storage.EF.Store;

namespace esdc_simulation_api
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
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Maternity Benefit Simulation API",
                    Description = "...",
                    TermsOfService = new Uri("https://example.com/terms")
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddMemoryCache();

            InjectSampleScenario(services);
            InjectMaternityBenefits(services);
            
            services.AddScoped<IJoinResults, Joiner>();
            
            services.AddScoped<IRestClient, RestSharp.RestClient>();

            // Rules options
            var rulesUrl = Configuration["RulesOptions:Url"] ?? 
                Environment.GetEnvironmentVariable("rulesUrl");

            var rulesOptions = new RulesOptions() {
                Url = rulesUrl
            };
            services.AddSingleton<IOptions<RulesOptions>>(x => Options.Create(rulesOptions));

            // DB injection
            string connectionString = Configuration.GetConnectionString("DefaultDB");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("esdc-simulation-api")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Apply migrations in prod
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    context.Database.Migrate();
                }
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/spec.json", "Maternity Benefit Simulation API Spec");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void InjectSampleScenario(IServiceCollection services) {
            services.AddScoped<IHandleSimulationRequests<SampleScenarioCaseRequest>, 
                SimulationRequestHandler<
                    SampleScenarioCase, 
                    SampleScenarioCaseRequest, 
                    SampleScenarioPerson
                >
            >();

            services.AddScoped<
                IBuildSimulations<
                    SampleScenarioCase, 
                    SampleScenarioCaseRequest
                >,
                SampleScenarioSimulationBuilder>(); 

            services.AddScoped<IRunSimulations<SampleScenarioCase, SampleScenarioPerson>,
                SimulationRunner<SampleScenarioCase, SampleScenarioPerson>>();

            services.AddScoped<IRunCases<SampleScenarioCase, SampleScenarioPerson>, 
                CaseRunner<SampleScenarioCase, SampleScenarioPerson>>();

            services.AddScoped<IExecuteRules<SampleScenarioCase, SampleScenarioPerson>,
                SampleScenarioExecutor>();

            services.AddScoped<IRulesEngine<SampleScenarioRulesRequest>, RulesApi<SampleScenarioRulesRequest>>();

            // Storage
            services.AddScoped<IStorePersons<SampleScenarioPerson>, SampleScenarioPersonStore>();
            services.AddScoped<IStoreSimulationResults<SampleScenarioCase>, SampleScenarioSimulationResultsStore>();
            services.AddScoped<IStoreSimulations<SampleScenarioCase>, SampleScenarioSimulationStore>();
        }

        private void InjectMaternityBenefits(IServiceCollection services) {
            services.AddScoped<IHandleSimulationRequests<MaternityBenefitsCaseRequest>, 
                SimulationRequestHandler<
                    MaternityBenefitsCase, 
                    MaternityBenefitsCaseRequest, 
                    MaternityBenefitsPerson
                >
            >();

            services.AddScoped<IHandlePersonCreationRequests<MaternityBenefitsPersonRequest>, 
                MaternityBenefitPersonCreationRequestHandler
            >();

            services.AddScoped<
                IBuildSimulations<
                    MaternityBenefitsCase, 
                    MaternityBenefitsCaseRequest
                >,
                MaternityBenefitsSimulationBuilder>(); 

            services.AddScoped<IRunSimulations<MaternityBenefitsCase, MaternityBenefitsPerson>,
                SimulationRunner<MaternityBenefitsCase, MaternityBenefitsPerson>>();

            services.AddScoped<IRunCases<MaternityBenefitsCase, MaternityBenefitsPerson>, 
                CaseRunner<MaternityBenefitsCase, MaternityBenefitsPerson>>();

            services.AddScoped<IExecuteRules<MaternityBenefitsCase, MaternityBenefitsPerson>,
                MaternityBenefitsExecutor>();

            services.AddScoped<IRulesEngine<MaternityBenefitsRulesRequest>, RulesApi<MaternityBenefitsRulesRequest>>();

            // EF Storage
            services.AddScoped<IStorePersons<MaternityBenefitsPerson>, MaternityBenefitsPersonEFStore>();
            services.AddScoped<IStoreSimulations<MaternityBenefitsCase>, MaternityBenefitsSimulationEFStore>();
            services.AddScoped<IStoreSimulationResults<MaternityBenefitsCase>, MaternityBenefitsSimulationResultsEFStore>();
            
            // Cache Storage
            // services.AddScoped<IStorePersons<MaternityBenefitsPerson>, MaternityBenefitsPersonStore>();
            // services.AddScoped<IStoreSimulations<MaternityBenefitsCase>, MaternityBenefitsSimulationStore>();
            // services.AddScoped<IStoreSimulationResults<MaternityBenefitsCase>, MaternityBenefitsSimulationResultsStore>();
        }

    }
}
