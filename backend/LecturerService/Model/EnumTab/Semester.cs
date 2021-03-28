using System.ComponentModel.DataAnnotations;

namespace LecturerService.Model.EnumTab
{
    public class Semester
    {
        [Key]
        public Data.Semester Type { get; set; }

        [Required(ErrorMessage = "Must specify semester type name!")]
        public string Name { get; set; }
    }
}