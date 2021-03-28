namespace LecturerService.Data
{
    public class CourseShort
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public CourseType TypeID { get; set; }
        
#nullable enable
        public string? LecturerID { get; set; }
        public string? CourseGroup { get; set; }
#nullable disable

        public CourseShort(Model.Course course)
        {
            ID = course.ID;
            Name = course.Name;
            TypeID = course.TypeID;
            LecturerID = course.LecturerID;
            CourseGroup = course.CourseGroup;
        }
    }
}