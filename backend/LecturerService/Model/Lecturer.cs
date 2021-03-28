using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("Role")]
        [Required(ErrorMessage = "Lecturer role cannot be empty!")]
        public Data.Role RoleTypeID { get; set; }
        public EnumTab.Role Role { get; set; }

#nullable enable
        public string? Phone { get; set; }
        public string? Title { get; set; }
#nullable disable

        public Lecturer() {}
        public Lecturer(Data.Lecturer lecturer)
        {
            ID = lecturer.ID;
            Name = lecturer.Name;
            Surname = lecturer.Surname;
            Mail = lecturer.Mail;
            RoleTypeID = lecturer.RoleTypeID;
            Phone = lecturer.Phone;
            Title = lecturer.Title;
        }
    }
}