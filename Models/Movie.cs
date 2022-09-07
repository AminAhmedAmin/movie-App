using System.ComponentModel.DataAnnotations;

namespace moveis.Models
{
    public class Movie
    {
        public int Id { get; set; }


        [Required, MaxLength(50)]
        public string Titel { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }


        [Required, MaxLength(2500)]
        public string Storeline { get; set; }

        [Required]
        public byte[] Poster { get; set; }

        public byte GanaraId { get; set; }
        public Ganara Ganara { get; set; }
        
    }
}
