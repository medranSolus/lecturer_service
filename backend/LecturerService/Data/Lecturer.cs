using System.ComponentModel.DataAnnotations;

namespace LecturerService.Data
{
    public class Lecturer
    {
        [Key]
        public string ID { get; set; }

        [Required(ErrorMessage = "Password hash cannot be empty!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password hash cannot be empty!")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Password hash cannot be empty!")]
        public string Mail { get; set; }

        public string Phone { get; set; }

        public string Title { get; set; }
    }
}