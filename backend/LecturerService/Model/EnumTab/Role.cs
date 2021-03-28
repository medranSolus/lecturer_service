using System.ComponentModel.DataAnnotations;

namespace LecturerService.Model.EnumTab
{
    public class Role
    {
        [Key]
        public Data.Role Type { get; set; }

        [Required(ErrorMessage = "Must specify role type name!")]
        public string Name { get; set; }
    }
}