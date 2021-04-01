using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LecturerService.Model
{
    public class CourseMsg
    {
        [Key]
        public long ID { get; set; }

        [ForeignKey("Course")]
        [Required(ErrorMessage = "Must specify course!")]
        public string CourseID { get; set; }
        public Course Course { get; set; }

        public CourseMsg() {}
        public CourseMsg(Data.CourseMsg msg)
        {
            ID = msg.ID;
            CourseID = msg.CourseID;
        }
    }
}