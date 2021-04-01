namespace LecturerService.Data
{
    public class CourseMsg
    {
        public long ID { get; set; }
        public CourseShort Course { get; set; }

        public CourseMsg(Model.CourseMsg msg)
        {
            ID = msg.ID;
            Course = new CourseShort(msg.Course);
        }
    }
}