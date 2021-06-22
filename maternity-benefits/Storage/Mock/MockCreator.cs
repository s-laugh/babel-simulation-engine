using System;
using System.Collections.Generic;

namespace maternity_benefits.Storage.Mock
{
    public static class MockCreator
    {
        private static readonly int MINIMUM_AGE = 16;
        private static readonly int MAXIMUM_AGE = 36;

        public static List<MaternityBenefitsPerson> GetMockPersons(int amount = 100) {
            var result = new List<MaternityBenefitsPerson>();

            for (int i = 0; i < amount; i++) {
                var nextPerson = GenerateMockPerson();
                result.Add(nextPerson);
            }
            return result;
        }

        private static MaternityBenefitsPerson GenerateMockPerson() {
            return new MaternityBenefitsPerson() {
                Id = Guid.NewGuid(),
                Age = GenerateAge(),
                Flsah = GenerateFlsah(),
                AverageIncome = GenerateIncome()
            };
        }

        private static int GenerateAge() {
            var rnd = new Random();
            return rnd.Next(MINIMUM_AGE, MAXIMUM_AGE);
        }

        private static string GenerateFlsah() {
            var rnd = new Random();
            var roll = rnd.Next(20);
            if (roll < 3) {
                return "Other";
            } else if (roll < 10) { 
                return "French";
            } else {
                return "English";
            }
        }

        private static decimal GenerateIncome() {
            var rnd = new Random();
            decimal baseAmount = rnd.Next(300, 1300);

            var roll = rnd.Next(1, 6);
            if (roll < 2) {
                baseAmount += 200;
            }
            else if (roll < 4) {
                baseAmount -= 100;
            }
            return baseAmount;
        }

    }
}