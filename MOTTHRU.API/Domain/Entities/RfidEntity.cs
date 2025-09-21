using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOTTHRU.API.Domain.Entities
{
    [Table("rfid")]
    public class RfidEntity
    {
        [Key]
        [Column("id_rfid")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo sinal é obrigatório")]
        [StringLength(15, ErrorMessage = "Sinal não pode ter mais que 15 caracteres")]
        [Column("sinal")]
        public string Sinal { get; set; } = string.Empty;

        [ForeignKey(nameof(Moto))]
        [Column("moto_id_moto")]
        public int MotoId { get; set; }

        public MotoEntity Moto { get; set; }
    }
}