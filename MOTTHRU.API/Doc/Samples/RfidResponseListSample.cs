using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Domain.Entities;
using Swashbuckle.AspNetCore.Filters;

namespace MOTTHRU.API.Doc.Samples
{
    public class RfidResponseListSample : IExamplesProvider<IEnumerable<RfidEntity>>
    {
        public IEnumerable<RfidEntity> GetExamples()
        {
            return new List<RfidEntity>
            {
                new RfidEntity {
                    Id = 1,
                    Sinal = "ABC123XYZ",
                    Moto = new MotoEntity { Id = 100, Placa = "AAA1A11" }
                },
                new RfidEntity {
                    Id = 2,
                    Sinal = "XYZ987ABC",
                    Moto = new MotoEntity { Id = 101, Placa = "BBB2B22" }
                }
            };
        }
    }

    public class RfidResponseSample : IExamplesProvider<RfidEntity>
    {
        public RfidEntity GetExamples()
        {
            return new RfidEntity
            {
                Id = 1,
                Sinal = "ABC123XYZ",
                Moto = new MotoEntity { Id = 100, Placa = "AAA1A11" }
            };
        }
    }

    public class RfidRequestSample : IExamplesProvider<RfidDto>
    {
        public RfidDto GetExamples()
        {
            return new RfidDto("ABC123XYZ", 100);
        }
    }
}
