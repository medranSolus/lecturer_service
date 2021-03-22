namespace LecturerService.Data
{
    public class Course
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public CourseType Type { get; set; }
#nullable enable
        public string? Group { get; set; }
        public string? LecturerID { get; set; }
#nullable disable

        public Course(Model.Course course)
        {
            ID = course.ID;
            Name = course.Name;
            Type = course.Type;
            Group = course.Group;
            LecturerID = course.LecturerID;
        }
    }
}