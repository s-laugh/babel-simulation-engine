using System;
using System.Collections.Generic;
using System.Linq;

namespace maternity_benefits
{
    public static class MockCreator
    {
        public static List<MaternityBenefitsPerson> GeneratePersons(int amount) {
            var result = new List<MaternityBenefitsPerson>();
            var rand = new Random();

            for (int i = 0; i < amount; i++) {
                var personId = Guid.NewGuid();
                var age = GenerateAge();

                var nextPerson = new MaternityBenefitsPerson() {
                    Id = personId,
                    AverageIncome = rand.Next(200, 1200),
                    Age = age,
                    Flsah = GenerateFslah(),
                };
                result.Add(nextPerson);
            }
            return result;
        }
    
        private static string GenerateFslah() {
            var result = "Other";
            var rand = new Random();

            var next = rand.Next(7);
            if (next <= 3) {
                result = "English";
            } else if (next <= 5) {
                result = "French";
            }
            return result;
        }

        private static int GenerateAge() {
            var rand = new Random();
            return rand.Next(16, 37);
        }
    }
}