namespace LecturerService.Data
{
    public class CourseShort
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string LecturerID { get; set; }
        public CourseType TypeID { get; set; }
        
#nullable enable
        public string? CourseGroup { get; set; }
#nullable disable

        public CourseShort(Model.Course course)
        {
            ID = course.ID;
            Name = course.Name;
            LecturerID = course.LecturerID;
            TypeID = course.TypeID;
            CourseGroup = course.CourseGroup;
        }
    }
}