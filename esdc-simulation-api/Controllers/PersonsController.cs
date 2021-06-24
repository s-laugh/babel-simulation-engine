using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Classes;
using esdc_simulation_base.Src.Storage;
using maternity_benefits;
using maternity_benefits.Storage.Mock;

namespace esdc_simulation_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IHandlePersonCreationRequests<MaternityBenefitsPersonRequest> _personRequestHandler;
        private readonly IStorePersons<MaternityBenefitsPerson> _personStore;

        public PersonsController(
            IHandlePersonCreationRequests<MaternityBenefitsPersonRequest> personRequestHandler,
            IStorePersons<MaternityBenefitsPerson> personStore
        )
        {
            _personRequestHandler = personRequestHandler;
            _personStore = personStore;
        }

        [HttpGet]
        public IEnumerable<MaternityBenefitsPerson> GetPersons()
        {
            // TODO: This may end up being too big a response. Maybe a summary?
            var persons = _personStore.GetAllPersons();
            return persons;
        }

        
        [HttpPost]
        public void AddPersons(List<MaternityBenefitsPersonRequest> personsRequest)
        {
            _personRequestHandler.Handle(personsRequest);
            return;
        }

        [HttpDelete]
        public void DeletePersons()
        {
            _personStore.Clear();
        }



        ///// Custom
        // TODO: Add a toggle/config//auth on here for more control

        [HttpGet("Mock")]
        public string MockSetup()
        {   
            var persons = _personStore.GetAllPersons();
            if (persons.Count() > 0) {
                throw new Exception("DB is populated. Cannot generate mocks.");
            }

            var mockPersons = MockCreator.GetMockPersons(100);
            _personStore.AddPersons(mockPersons);

            return "Mock Persons generated";
        }

    }
}
