using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOTTHRU.API.Domain.Entities
{
    [Table("moto")]
    public class MotoEntity
    {
        [Key]
        [Column("id_moto")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo placa é obrigatório")]
        [StringLength(7, ErrorMessage = "Placa não pode ter mais que 7 caracteres")]
        [Column("placa")]
        public string Placa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo chassi é obrigatório")]
        [StringLength(17, ErrorMessage = "Chassi não pode ter mais que 17 caracteres")]
        [Column("chassi")]
        public string Chassi { get; set; } = string.Empty;

        [Column("num_motor")]
        [StringLength(20, ErrorMessage = "Número do motor não pode ter mais que 20 caracteres")]
        public string? NumMotor { get; set; }

        
        // TODO: Implementar entidade "Modelo"
        // [ForeignKey(nameof(Modelo))]
        // [Column("modelo_id_modelo")]
        // public int ModeloId { get; set; }
        // public ModeloEntity Modelo { get; set; }

        [ForeignKey(nameof(Patio))]
        [Column("patio_id_patio")]
        public int PatioId { get; set; }
        public PatioEntity Patio { get; set; }
    }
}