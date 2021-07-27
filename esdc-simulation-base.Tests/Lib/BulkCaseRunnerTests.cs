using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;
using FakeItEasy;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Classes;

namespace esdc_simulation_base.Tests.Lib
{
    public class BulkCaseRunnerTests
    {
        
        [Fact]
        public void ShouldWorkNormally()
        {
            // Arrange
            var executor = A.Fake<IExecuteBulkRules<ISimulationCase, IPerson>>();
            
            int count = 201;
            int maxBulk = 100;
            var rnd = new Random();
            var persons = new List<IPerson>();
            var resultSet = new Dictionary<Guid, decimal>();
            for (int i = 0; i < count; i++) {
                var nextGuid = Guid.NewGuid();
                var nextPerson = A.Fake<IPerson>();
                nextPerson.Id = nextGuid;
                persons.Add(nextPerson);

                decimal nextVal = (Decimal)rnd.NextDouble() * 1000;
                resultSet.Add(nextGuid, nextVal);       
            }

            var resultSet1 = resultSet.Skip(0).Take(maxBulk).ToDictionary(x => x.Key, x => x.Value);
            var resultSet2 = resultSet.Skip(maxBulk).Take(maxBulk).ToDictionary(x => x.Key, x => x.Value);
            var resultSet3 = resultSet.Skip(maxBulk * 2).Take(maxBulk).ToDictionary(x => x.Key, x => x.Value);

            A.CallTo(() => executor.Execute(A<ISimulationCase>._, A<IEnumerable<IPerson>>._))
                .Returns(resultSet1)
                .Once().Then
                .Returns(resultSet2)
                .Once().Then
                .Returns(resultSet3);

            // Act
            var sut = new BulkCaseRunner<ISimulationCase, IPerson>(executor);
            var simulationCase = A.Fake<ISimulationCase>();
            var result = sut.Run(simulationCase, persons);

            // Assert
            A.CallTo(() => executor.Execute(A<ISimulationCase>._, A<IEnumerable<IPerson>>._))
                .MustHaveHappened(3, Times.Exactly);

            Assert.Equal(count, result.ResultSet.Count);
        }


    }
}
