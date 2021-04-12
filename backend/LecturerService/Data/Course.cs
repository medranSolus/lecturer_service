namespace LecturerService.Data
{
    public class Course
    {
        public string ID { get; set; }
        public bool Accepted { get; set; }

#region Basic info
        public string Name { get; set; }
        public Department DepartmentID { get; set; }
        public CourseType TypeID { get; set; }
        public Lang LanguageTypeID { get; set; }
        public byte Ects { get; set; }
        public byte HoursUniversity { get; set; }
        public byte HoursStudent { get; set; }
        public Semester SemesterTypeID { get; set; }
        public uint Year { get; set; }
#endregion // Basic info

#nullable enable
        public string? LecturerID { get; set; }
        public string? LecturerName { get; set; }
        public string? CourseGroup { get; set; }
#nullable disable

        public Course() {}
        public Course(Model.Course course)
        {
            ID = course.ID;
            Accepted = course.Accepted;
            Name = course.Name;
            DepartmentID = course.DepartmentID;
            TypeID = course.TypeID;
            LanguageTypeID = course.LanguageTypeID;
            Ects = course.Ects;
            HoursUniversity = course.HoursUniversity;
            HoursStudent = course.HoursStudent;
            SemesterTypeID = course.SemesterTypeID;
            Year = course.Year;
            LecturerID = course.LecturerID;
            if (LecturerID != null)
                LecturerName = course.Lecturer.Name + " " + course.Lecturer.Surname;
            else
                LecturerName = null;
            CourseGroup = course.CourseGroup;
        }
    }
}