namespace LecturerService.Data
{
    public class Group
    {
        public string ID { get; set; }
        public string CourseID { get; set; }
        public System.UInt16 StudentsCount { get; set; }

#region Loctaion
        public string Room { get; set; }
        public string Building { get; set; }
#endregion // Location

#region Time
        public WeekType WeekTypeID { get; set; }
        public byte StartMonth { get; set; }
        public byte StartDay { get; set; }
        public byte EndMonth { get; set; }
        public byte EndDay { get; set; }
        public Day DayID { get; set; }
        public byte StartHour { get; set; }
        public byte StartMinute { get; set; }
        public byte EndHour { get; set; }
        public byte EndMinute { get; set; }
#endregion // Time
        
#nullable enable
        public string? LecturerID { get; set; }
        public string? CourseName { get; set; }
        public CourseType? CourseTypeID { get; set; }
#nullable disable

        public Group() {}
        public Group(Model.Group group)
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
            CourseName = group.Course.Name;
            CourseTypeID = group.Course.TypeID;
        }
    }
}
