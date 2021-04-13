using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Classes;
using esdc_simulation_base.Src.Storage;
using sample_scenario;
using maternity_benefits;

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
            services.AddMemoryCache();

            InjectSampleScenario(services);
            InjectMaternityBenefits(services);
            services.AddScoped<IJoinResults, Joiner>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            //services.AddScoped<IGetSimulations<SampleScenarioCase>, SampleScenarioPerson>();
            //services.AddScoped<ISimulationLib<MotorVehicleSimulationCase>, SimulationLib<MotorVehicleSimulationCase>>();

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

            //services.AddScoped<IGetSimulations<SampleScenarioCase>, SampleScenarioPerson>();
            //services.AddScoped<ISimulationLib<MotorVehicleSimulationCase>, SimulationLib<MotorVehicleSimulationCase>>();

            // Storage
            services.AddScoped<IStorePersons<MaternityBenefitsPerson>, MaternityBenefitsPersonStore>();
            services.AddScoped<IStoreSimulations<MaternityBenefitsCase>, MaternityBenefitsSimulationStore>();
            services.AddScoped<IStoreSimulationResults<MaternityBenefitsCase>, MaternityBenefitsSimulationResultsStore>();
            services.AddScoped<IStoreUnemploymentRegions, UnemploymentRegionStore>();
        }
    }
}
