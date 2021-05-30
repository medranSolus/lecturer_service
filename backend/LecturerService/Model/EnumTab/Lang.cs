using System.ComponentModel.DataAnnotations;

namespace LecturerService.Model.EnumTab
{
    public class Lang
    {
        [Key]
        public Data.Lang Type { get; set; }

        [Required(ErrorMessage = "Must specify lang type name!")]
        public string Name { get; set; }
    }
}