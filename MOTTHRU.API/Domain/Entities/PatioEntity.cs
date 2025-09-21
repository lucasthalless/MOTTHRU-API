using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOTTHRU.API.Domain.Entities
{
    [Table("patio")]
    public class PatioEntity
    {
        [Key]
        [Column("id_patio")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo nome_patio é obrigatório")]
        [StringLength(60, ErrorMessage = "Nome do pátio não pode ter mais que 60 caracteres")]
        [Column("nome_patio")]
        public string NomePatio { get; set; } = string.Empty;

        // TODO: implementar entidade "Endereco"
        // [ForeignKey(nameof(Endereco))]
        // [Column("endereco_id_endereco")]
        // public int EnderecoId { get; set; }

        // public EnderecoEntity Endereco { get; set; }

        public ICollection<MotoEntity>? Motos { get; set; }
    }
}