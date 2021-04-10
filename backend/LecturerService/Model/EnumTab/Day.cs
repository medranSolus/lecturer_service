using System.ComponentModel.DataAnnotations;

namespace LecturerService.Model.EnumTab
{
    public class Day
    {
        [Key]
        public Data.Day Type { get; set; }

        [Required(ErrorMessage = "Must specify day type name!")]
        public string Name { get; set; }
    }
}