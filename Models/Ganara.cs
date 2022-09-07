using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace moveis.Models
{
    public class Ganara
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public byte Id { get; set; }
        [Required ,MaxLength(50)]
        public string Name { get; set; }
    }
}
