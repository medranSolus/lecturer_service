using System.ComponentModel.DataAnnotations;

namespace LecturerService.Model.EnumTab
{
    public class WeekType
    {
        [Key]
        public Data.WeekType Type { get; set; }

        [Required(ErrorMessage = "Must specify week type name!")]
        public string Name { get; set; }
    }
}