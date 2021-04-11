namespace LecturerService.Data
{
    // Bits 7-0: Day, bits 15-8: Month
    using PackedDate = System.UInt16;

    public class Course
    {
        public string ID { get; set; }
        public bool Accepted { get; set; }

#region Basic info
        public string Name { get; set; }
        public int DepartmentID { get; set; }
        public string Department { get; set; }
        public Data.CourseType TypeID { get; set; }
        public Data.Lang LanguageTypeID { get; set; }
        public byte Ects { get; set; }
        public byte HoursUniversity { get; set; }
        public byte HoursStudent { get; set; }
#endregion // Basic info

#region Dates
        public Data.Semester SemesterTypeID { get; set; }
        public Data.WeekType WeekTypeID { get; set; }
        public uint Year { get; set; }
        public byte StartMonth { get; set; }
        public byte StartDay { get; set; }
        public byte EndMonth { get; set; }
        public byte EndDay { get; set; }
#endregion // Dates

#nullable enable
        public string? LecturerID { get; set; }
        public string? CourseGroup { get; set; }
#nullable disable

        public Course() {}
        public Course(Model.Course course)
        {
            ID = course.ID;
            Accepted = course.Accepted;
            Name = course.Name;
            DepartmentID = course.DepartmentID;
            Department = course.Department.Name;
            TypeID = course.TypeID;
            LanguageTypeID = course.LanguageTypeID;
            Ects = course.Ects;
            HoursUniversity = course.HoursUniversity;
            HoursStudent = course.HoursStudent;
            SemesterTypeID = course.SemesterTypeID;
            Year = course.Year;
            StartMonth = course.StartMonth;
            StartDay = course.StartDay;
            EndMonth = course.EndMonth;
            EndDay = course.EndDay;
            LecturerID = course.LecturerID;
            CourseGroup = course.CourseGroup;
        }
    }
}