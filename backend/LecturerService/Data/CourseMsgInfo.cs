namespace LecturerService.Data
{
    public class CourseMsgInfo
    {
        public long ID { get; set; }
        public CourseShort Course { get; set; }

        public CourseMsgInfo() {}
        public CourseMsgInfo(Model.CourseMsg msg)
        {
            ID = msg.ID;
            Course = new CourseShort(msg.Course);
        }
    }
}