using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Classes;
using sample_scenario;


namespace esdc_simulation_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {
        private readonly IHandleSimulationRequests<SampleScenarioCaseRequest> _requestHandler;

        public SampleController(IHandleSimulationRequests<SampleScenarioCaseRequest> requestHandler)
        {
            _requestHandler = requestHandler;
        }

        [HttpGet]
        public string Get()
        {
            return "Hello";
        }

        [HttpPost]
        public Guid Post(SimulationRequest<SampleScenarioCaseRequest> request)
        {
            var simulationId = _requestHandler.Handle(request);
            return simulationId;
        }

    }
}
