using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using esdc_simulation_base.Src.Lib;
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
            var persons = _personStore.GetAllPersons();
            return persons;
        }

        
        [HttpPost]
        public void AddPersons(List<MaternityBenefitsPersonRequest> personsRequest)
        {
            _personRequestHandler.Handle(personsRequest);
            return;
        }

        // TODO: Make sure this works properly
        [HttpDelete]
        public void DeletePersons()
        {
            _personStore.Clear();
        }



        // TODO: Add a toggle/config//auth on here for more control

        [HttpGet("Mock")]
        public string MockSetup()
        {   
            var numberOfMocks = 100;
            var persons = _personStore.GetAllPersons();
            if (persons.Count() > 0) {
                throw new Exception("DB is populated. Cannot generate mocks.");
            }

            var mockPersons = MockCreator.GetMockPersons(numberOfMocks);
            _personStore.AddPersons(mockPersons);

            return $"{numberOfMocks} Mock Persons generated";
        }

    }
}
