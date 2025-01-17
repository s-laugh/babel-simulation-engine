﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Storage;
using esdc_simulation_base.Src.Classes;

using maternity_benefits;
using maternity_benefits.Storage.Mock;

using esdc_simulation_classes.MaternityBenefits;

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
        public ActionResult<IEnumerable<MaternityBenefitsPerson>> GetPersons()
        {
            var persons = _personStore.GetAllPersons();
            return Ok(persons);
        }

        
        [HttpPost]
        public ActionResult AddPersons(List<MaternityBenefitsPersonRequest> personsRequest)
        {
            _personRequestHandler.Handle(personsRequest);
            return Ok();
        }

        [HttpDelete]
        public ActionResult DeletePersons()
        {
            _personStore.Clear();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePerson(Guid id)
        {
            try {
                _personStore.DeletePerson(id);
                return Ok();
            } catch (NotFoundException e) {
                return BadRequest(e.Message);
            } 
        }


        [HttpGet("Mock")]
        public ActionResult<string> MockSetup()
        {   
            var numberOfMocks = 100;
            var persons = _personStore.GetAllPersons();
            if (persons.Count() > 0) {
                return BadRequest(new { message = "DB is populated. Cannot generate mocks."});
            }

            var mockPersons = MockCreator.GetMockPersons(numberOfMocks);
            _personStore.AddPersons(mockPersons);

            return $"{numberOfMocks} Mock Persons generated";
        }

    }
}
