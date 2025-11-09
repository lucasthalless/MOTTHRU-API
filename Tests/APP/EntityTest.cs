using System.ComponentModel.DataAnnotations;
using MOTTHRU.API.Domain.Entities;

namespace Tests.APP {
    public class EntityTest
    {
        [Fact]  
        [Trait("Entity", "Patio")]  
        public void Patio_Valido_DeveSerValido()  
        {  
            // Arrange  
            var cliente = new PatioEntity
            {  
                Id = 1,  
                NomePatio = new string('A', 60)
            };  
  
            // Act  
            var results = ValidationHelper.ValidateObject(cliente);  
  
            // Assert  
            Assert.Empty(results);  
            Assert.False(results.Any(), "Nome do pátio não pode ter mais que 60 caracteres"); 
            Assert.False(results.Any(), "Campo nome_patio é obrigatório"); 
        }
        
        [Fact]  
        [Trait("Entity", "Patio")]  
        public void Patio_Invalido_DeveSerInvalido()  
        {  
            // Arrange  
            var cliente = new PatioEntity
            {  
                Id = 1,  
                NomePatio = "",
            };  
  
            // Act  
            var results = ValidationHelper.ValidateObject(cliente);  
  
            // Assert
            Assert.Contains(results, r => r.MemberNames.Contains("NomePatio") && r.ErrorMessage!.Contains("obrigatório"));
        }

        
        [Fact]  
        [Trait("Entity", "Moto")]  
        public void Moto_Valida_DeveSerValida()  
        {  
            // Arrange  
            var cliente = new MotoEntity
            {  
                Id = 1,  
                Placa = new string('A', 7),
                Chassi = new string('A', 17),
                NumMotor = new string('A', 20),
                PatioId = 1
            };  
  
            // Act  
            var results = ValidationHelper.ValidateObject(cliente);  
  
            // Assert  
            Assert.Empty(results);  
            Assert.False(results.Any(), "Campo placa é obrigatório"); 
            Assert.False(results.Any(), "Campo chassi é obrigatório"); 
            Assert.False(results.Any(), "Número do motor não pode ter mais que 20 caracteres"); 
        }
        
        [Fact]  
        [Trait("Entity", "Moto")]  
        public void Moto_Invalida_DeveSerInvalida()  
        {  
            // Arrange  
            var cliente = new MotoEntity
            {  
                Id = 1,  
                Placa = "",
                Chassi = "",
                NumMotor = new string('A', 20),
                PatioId = 1
            };  
  
            // Act  
            var results = ValidationHelper.ValidateObject(cliente);  
  
            // Assert
            Assert.Contains(results, r => r.MemberNames.Contains("Placa") && r.ErrorMessage!.Contains("obrigatório"));
            Assert.Contains(results, r => r.MemberNames.Contains("Chassi") && r.ErrorMessage!.Contains("obrigatório"));
        }
        
        

        
        [Fact]  
        [Trait("Entity", "Rfid")]  
        public void Rfid_Valido_DeveSerValido()  
        {  
            // Arrange  
            var cliente = new RfidEntity
            {  
                Id = 1,  
                Sinal =  new string('A', 15),
            };  
  
            // Act  
            var results = ValidationHelper.ValidateObject(cliente);  
  
            // Assert  
            Assert.Empty(results);  
            Assert.False(results.Any(), "Campo sinal é obrigatório"); 
        }
        
        [Fact]  
        [Trait("Entity", "Rfid")]  
        public void Rfid_Invaido_DeveSerInvalido()  
        {  
            // Arrange  
            var cliente = new RfidEntity
            {  
                Id = 1,  
                Sinal = "",
            };  
  
            // Act  
            var results = ValidationHelper.ValidateObject(cliente);  
  
            // Assert
            Assert.Contains(results, r => r.MemberNames.Contains("Sinal") && r.ErrorMessage!.Contains("obrigatório"));
        }
    }

    public static class ValidationHelper  
    {  
        public static IList<ValidationResult> ValidateObject(object instance)  
        {  
            var results = new List<ValidationResult>();  
              
            var ctx = new ValidationContext(instance);  
      
            Validator.TryValidateObject(instance, ctx, results, validateAllProperties: true);  
      
            return results;  
        }  
    }
}