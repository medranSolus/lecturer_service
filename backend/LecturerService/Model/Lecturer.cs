using System.ComponentModel.DataAnnotations;

namespace LecturerService.Model
{
    public class Lecturer
    {
        [Key]
        public string ID { get; set; }

        [Required(ErrorMessage = "Lecturer name cannot be empty!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lecturer surname cannot be empty!")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Lecturer email cannot be empty!")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Lecturer role cannot be empty!")]
        public Data.Role Role { get; set; }

#nullable enable
        public string? Phone { get; set; }
        public string? Title { get; set; }
#nullable disable
    }
}