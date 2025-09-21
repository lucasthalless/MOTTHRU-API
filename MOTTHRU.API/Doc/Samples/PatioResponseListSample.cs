using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Domain.Entities;
using Swashbuckle.AspNetCore.Filters;

namespace MOTTHRU.API.Doc.Samples {
    public class PatioResponseListSample : IExamplesProvider<IEnumerable<PatioEntity>>
    {
        public IEnumerable<PatioEntity> GetExamples()
        {
            return new List<PatioEntity>
            {
                new PatioEntity {
                    Id = 1,
                    NomePatio = "Patio Central",
                    // Endereco = new EnderecoEntity { Id = 10, Rua = "Rua A, 123", Cidade = "São Paulo" }
                },
                new PatioEntity {
                    Id = 2,
                    NomePatio = "Patio Zona Leste",
                    // Endereco = new EnderecoEntity { Id = 11, Rua = "Rua B, 456", Cidade = "São Paulo" }
                }
            };
        }
    }

    public class PatioResponseSample : IExamplesProvider<PatioEntity>
    {
        public PatioEntity GetExamples()
        {
            return new PatioEntity
            {
                Id = 1,
                NomePatio = "Patio Central",
                // Endereco = new EnderecoEntity { Id = 10, Rua = "Rua A, 123", Cidade = "São Paulo" }
            };
        }
    }

    public class PatioRequestSample : IExamplesProvider<PatioDto>
    {
        public PatioDto GetExamples()
        {
            return new PatioDto("Patio Central");
        }
    }
}