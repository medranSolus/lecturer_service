namespace LecturerService.Data
{
    public class CourseShort
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public Department DepartmentID { get; set; }
        public CourseType TypeID { get; set; }
        
#nullable enable
        public string? LecturerID { get; set; }
        public string? LecturerName { get; set; }
        public string? CourseGroup { get; set; }
#nullable disable

        public CourseShort(Model.Course course)
        {
            ID = course.ID;
            Name = course.Name;
            DepartmentID = course.DepartmentID;
            TypeID = course.TypeID;
            LecturerID = course.LecturerID;
            if (LecturerID != null)
                LecturerName = course.Lecturer.Name + " " + course.Lecturer.Surname;
            else
                LecturerName = null;
            CourseGroup = course.CourseGroup;
        }
    }
}