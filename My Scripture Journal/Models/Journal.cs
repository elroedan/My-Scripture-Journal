using System.ComponentModel.DataAnnotations;

namespace My_Scripture_Journal.Models
{
    public class Journal
    {
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Scripture { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Book { get; set; }
        public int Chapter { get; set; }
        [Range(1, 100)]
        public int Start { get; set; }
        [Range(1, 100)]
        public int End { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Text { get; set; }

        [Display(Name = "Date Added")]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }
    }
}
