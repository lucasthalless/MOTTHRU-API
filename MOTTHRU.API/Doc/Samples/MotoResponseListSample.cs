using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Domain.Entities;
using Swashbuckle.AspNetCore.Filters;

namespace MOTTHRU.API.Doc.Samples
{
    public class MotoResponseListSample : IExamplesProvider<IEnumerable<MotoEntity>>
    {
        public IEnumerable<MotoEntity> GetExamples()
        {
            return new List<MotoEntity>
            {
                new MotoEntity {
                    Id = 100,
                    Placa = "AAA1A11",
                    Chassi = "CHASSI1234567890",
                    NumMotor = "MOTOR123",
                    PatioId = 1
                },
                new MotoEntity {
                    Id = 101,
                    Placa = "BBB2B22",
                    Chassi = "CHASSI9876543210",
                    NumMotor = "MOTOR456",
                    PatioId = 2
                }
            };
        }
    }

    public class MotoResponseSample : IExamplesProvider<MotoEntity>
    {
        public MotoEntity GetExamples()
        {
            return new MotoEntity
            {
                Id = 100,
                Placa = "AAA1A11",
                Chassi = "CHASSI1234567890",
                NumMotor = "MOTOR123",
                PatioId = 1
            };
        }
    }

    public class MotoRequestSample : IExamplesProvider<MotoDto>
    {
        public MotoDto GetExamples()
        {
            return new MotoDto(
                Placa: "AAA1A11",
                Chassi: "CHASSI1234567890",
                NumMotor: "MOTOR123",
                PatioId: 1
            );
        }
    }
}