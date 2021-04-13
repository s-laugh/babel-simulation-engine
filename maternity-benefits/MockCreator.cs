using System;
using System.Collections.Generic;
using System.Linq;

namespace maternity_benefits
{
    public static class MockCreator
    {
        public static List<UnemploymentRegion> GetUnemploymentRegions() {
            return new List<UnemploymentRegion>() {
                new UnemploymentRegion() {
                    Id = Guid.NewGuid(),
                    Code = "01",
                    Province = "NL",
                    Name = "St. Johns",
                    UnemploymentRate = 13.1,
                    BestWeeks = 14
                },
                new UnemploymentRegion() {
                    Id = Guid.NewGuid(),
                    Code = "65",
                    Province = "PEI",
                    Name = "Charlottetown",
                    UnemploymentRate = 13.1,
                    BestWeeks = 14
                },
                new UnemploymentRegion() {
                    Id = Guid.NewGuid(),
                    Code = "66",
                    Province = "PEI",
                    Name = "Prince Edward Island",
                    UnemploymentRate = 13.1,
                    BestWeeks = 14
                },
                new UnemploymentRegion() {
                    Id = Guid.NewGuid(),
                    Code = "04",
                    Province = "NS",
                    Name = "Eastern Nova Scotia",
                    UnemploymentRate = 13.1,
                    BestWeeks = 14
                },
                new UnemploymentRegion() {
                    Id = Guid.NewGuid(),
                    Code = "05",
                    Province = "NS",
                    Name = "Western Nova Scotia",
                    UnemploymentRate = 13.1,
                    BestWeeks = 14
                },
                new UnemploymentRegion() {
                    Id = Guid.NewGuid(),
                    Code = "06",
                    Province = "NS",
                    Name = "Halifax",
                    UnemploymentRate = 13.1,
                    BestWeeks = 14
                },
                new UnemploymentRegion() {
                    Id = Guid.NewGuid(),
                    Code = "12",
                    Province = "QC",
                    Name = "Trois Rivieres",
                    UnemploymentRate = 13.1,
                    BestWeeks = 15
                },
                new UnemploymentRegion() {
                    Id = Guid.NewGuid(),
                    Code = "22",
                    Province = "ON",
                    Name = "Ottawa",
                    UnemploymentRate = 13.1,
                    BestWeeks = 18
                },
                new UnemploymentRegion() {
                    Id = Guid.NewGuid(),
                    Code = "23",
                    Province = "ON",
                    Name = "Eastern Ontario",
                    UnemploymentRate = 13.1,
                    BestWeeks = 18
                },
                new UnemploymentRegion() {
                    Id = Guid.NewGuid(),
                    Code = "24",
                    Province = "ON",
                    Name = "Kingston",
                    UnemploymentRate = 13.1,
                    BestWeeks = 18
                },
                new UnemploymentRegion() {
                    Id = Guid.NewGuid(),
                    Code = "39",
                    Province = "MN",
                    Name = "Winnipeg",
                    UnemploymentRate = 13.1,
                    BestWeeks = 18
                },

                new UnemploymentRegion() {
                    Id = Guid.NewGuid(),
                    Code = "42",
                    Province = "SK",
                    Name = "Regina",
                    UnemploymentRate = 13.1,
                    BestWeeks = 14
                },
                new UnemploymentRegion() {
                    Id = Guid.NewGuid(),
                    Code = "46",
                    Province = "AB",
                    Name = "Calgary",
                    UnemploymentRate = 13.1,
                    BestWeeks = 14
                },
                new UnemploymentRegion() {
                    Id = Guid.NewGuid(),
                    Code = "48",
                    Province = "AB",
                    Name = "Northern Alberta",
                    UnemploymentRate = 13.1,
                    BestWeeks = 14
                },
                new UnemploymentRegion() {
                    Id = Guid.NewGuid(),
                    Code = "51",
                    Province = "BC",
                    Name = "Abbotsford",
                    UnemploymentRate = 13.1,
                    BestWeeks = 14
                },
                new UnemploymentRegion() {
                    Id = Guid.NewGuid(),
                    Code = "53",
                    Province = "BC",
                    Name = "Victoria",
                    UnemploymentRate = 13.1,
                    BestWeeks = 14
                },
            };
        }

        public static List<MaternityBenefitsPerson> GeneratePersons(List<UnemploymentRegion> regions, int amount) {
            var result = new List<MaternityBenefitsPerson>();
            var rand = new Random();

            for (int i = 0; i < amount; i++) {
                int regionIndex = rand.Next(regions.Count);

                var personId = Guid.NewGuid();
                var age = GenerateAge();

                var nextPerson = new MaternityBenefitsPerson() {
                    Id = personId,
                    UnemploymentRegion = regions[regionIndex],
                    WeeklyIncome = GenerateWeeklyIncomes(age),
                    Age = age,
                    Flsah = GenerateFslah(),
                };
                result.Add(nextPerson);
            }
            return result;
        }

        private static List<WeeklyIncome> GenerateWeeklyIncomes(int age) {
            var result = new List<WeeklyIncome>();
            var rand = new Random();

            // pick a base salary, partly based off age
            decimal baseSalary = (rand.Next(15000, 80000) + ((age - 20) * 500)) / 52;
            int diff = Convert.ToInt32(baseSalary * 0.13m);

            for (int i = 0; i < 75; i++) {
                var next = new WeeklyIncome() {
                    StartDate = DateTime.Now.AddDays((58 - i) * -7),
                    Income = baseSalary + rand.Next(-diff, diff), // Should be informed by data
                };
                result.Add(next);
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