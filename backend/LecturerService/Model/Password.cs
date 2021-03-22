using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LecturerService.Model
{
    public class Password
    {
        [Key]
        [ForeignKey("Lecturer")]
        public string ID { get; set; }
        public Lecturer Lecturer { get; set; }

        [Required(ErrorMessage = "Password hash cannot be empty!")]
        public string Hash { get; set; }
    }
}