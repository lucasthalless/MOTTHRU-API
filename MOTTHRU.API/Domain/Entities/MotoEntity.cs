using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOTTHRU.API.Domain.Entities
{
    [Table("moto")]
    public class MotoEntity
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string placa {  get; set; } = String.Empty;
        public string chassi { get; set; }
        public string num_motor { get; set; }
    }
}