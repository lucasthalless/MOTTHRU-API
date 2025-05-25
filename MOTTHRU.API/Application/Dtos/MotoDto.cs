using System.ComponentModel.DataAnnotations;

namespace MOTTHRU.API.Application.Dtos
{
    public class MotoDto
    {
        [Required(ErrorMessage = $"Campo {nameof(placa)} é obrigatório")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "A placa deve conter exatamente 7 caracteres")]
        public string placa { get; set; } = string.Empty;

        [Required(ErrorMessage = $"Campo {nameof(chassi)} é obrigatório")]
        [StringLength(17, MinimumLength = 17, ErrorMessage = "O chassi deve conter exatamente 17 caracteres")]
        public string chassi { get; set; }

        [Required(ErrorMessage = $"Campo {nameof(num_motor)} é obrigatório")]
        public string num_motor { get; set; }
        
        [Required(ErrorMessage = $"Campo {nameof(status)} é obrigatório")]
        public string status { get; set; } = string.Empty;
    }
}