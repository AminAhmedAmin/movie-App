using moveis.Models;
using System.ComponentModel.DataAnnotations;

namespace movies.Viewmodel
{
    public class Moviefromviewmodel
    {
        public int Id { get; set; }


        [Required, StringLength(50)]
        public string Titel { get; set; }

        public int Year { get; set; }
        [Range(1,10)]
        public double Rate { get; set; }


        [Required, StringLength(2500)]
        public string Storeline { get; set; }

        
        public byte[] Poster { get; set; }
        [Display (Name = "Ganra")]
        public byte GanaraId { get; set; }

        public IEnumerable<Ganara> Genares { get; set; } 
       

    }
}
