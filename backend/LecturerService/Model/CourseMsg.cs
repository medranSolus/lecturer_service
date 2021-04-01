using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LecturerService.Model
{
    public class CourseMsg
    {
        [Key]
        public long ID { get; set; }

        [ForeignKey("Course")]
        [Required(ErrorMessage = "Must specify course of group!")]
        public string CourseID { get; set; }
        public Course Course { get; set; }
    }
}