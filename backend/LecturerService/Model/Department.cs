using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LecturerService.Model
{
    public class Department
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name cannot be empty!")]
        public string Name { get; set; }
    }
}