using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LecturerService.Model.EnumTab
{
    public class Department
    {
        [Key]
        public Data.Department Type { get; set; }

        [Required(ErrorMessage = "Name cannot be empty!")]
        public string Name { get; set; }
    }
}