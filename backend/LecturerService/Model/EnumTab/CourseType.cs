using System.ComponentModel.DataAnnotations;

namespace LecturerService.Model.EnumTab
{
    public class CourseType
    {
        [Key]
        public Data.CourseType Type { get; set; }

        [Required(ErrorMessage = "Must specify course type name!")]
        public string Name { get; set; }
    }
}