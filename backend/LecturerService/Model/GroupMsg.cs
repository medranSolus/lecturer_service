using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LecturerService.Model
{
    public class GroupMsg
    {
        [Key]
        public long ID { get; set; }

        [ForeignKey("Group")]
        [Required(ErrorMessage = "Must specify group of course!")]
        public string GroupID { get; set; }
        public Group Group { get; set; }

        [ForeignKey("Lecturer")]
        [Required(ErrorMessage = "Must specify lecturer!")]
        public string LecturerID { get; set; }
        public Lecturer Lecturer { get; set; }

        public GroupMsg() {}
        public GroupMsg(Data.GroupMsg msg)
        {
            Update(msg);
        }

        public void Update(Data.GroupMsg msg)
        {
            ID = msg.ID;
            GroupID = msg.GroupID;
            LecturerID = msg.LecturerID;
        }
    }
}