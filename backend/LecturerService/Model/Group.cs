using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LecturerService.Model
{
    public class Group
    {
        [Key]
        public string ID { get; set; }

        [ForeignKey("Course")]
        [Required(ErrorMessage = "Must specify course of group!")]
        public string CourseID { get; set; }
        public Course Course { get; set; }

        [Required(ErrorMessage = "Must specify max size of a course group!")]
        public System.UInt16 StudentsCount { get; set; }

#region Loctaion
        [Required(ErrorMessage = "Must specify room of a course group!")]
        public string Room { get; set; }

        [Required(ErrorMessage = "Must specify bulding of a course group!")]
        public string Building { get; set; }
#endregion // Location

#region Time
        [ForeignKey("WeekType")]
        [Required(ErrorMessage = "Must specify week of course group!")]
        public Data.WeekType WeekTypeID { get; set; }
        public EnumTab.WeekType WeekType { get; set; }

        [Required(ErrorMessage = "Starting month of course cannot be empty!")]
        public byte StartMonth { get; set; }

        [Required(ErrorMessage = "Starting day of course cannot be empty!")]
        public byte StartDay { get; set; }

        [Required(ErrorMessage = "Ending month of course cannot be empty!")]
        public byte EndMonth { get; set; }

        [Required(ErrorMessage = "Ending day of course cannot be empty!")]
        public byte EndDay { get; set; }

        [ForeignKey("Day")]
        [Required(ErrorMessage = "Must week day of course group!")]
        public Data.Day DayID { get; set; }
        public EnumTab.Day Day { get; set; }

        [Required(ErrorMessage = "Starting hour of group cannot be empty!")]
        public byte StartHour { get; set; }

        [Required(ErrorMessage = "Starting minute of group cannot be empty!")]
        public byte StartMinute { get; set; }

        [Required(ErrorMessage = "Ending hour of group cannot be empty!")]
        public byte EndHour { get; set; }

        [Required(ErrorMessage = "Ending minute of group cannot be empty!")]
        public byte EndMinute { get; set; }
#endregion // Time
        
#nullable enable
        [ForeignKey("Lecturer")]
        public string? LecturerID { get; set; }
        public Lecturer? Lecturer { get; set; }
#nullable disable

        public Group() {}
        public Group(Data.Group group)
        {
            Update(group);
        }

        public void Update(Data.Group group)
        {
            ID = group.ID;
            CourseID = group.CourseID;
            StudentsCount = group.StudentsCount;
            Room = group.Room;
            Building = group.Building;
            WeekTypeID = group.WeekTypeID;
            StartMonth = group.StartMonth;
            StartDay = group.StartDay;
            EndMonth = group.EndMonth;
            EndDay = group.EndDay;
            DayID = group.DayID;
            StartHour = group.StartHour;
            StartMinute = group.StartMinute;
            EndHour = group.EndHour;
            EndMinute = group.EndMinute;
            LecturerID = group.LecturerID;
        }
    }
}