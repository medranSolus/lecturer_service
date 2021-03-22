namespace LecturerService.Data
{
    public enum Role : byte { Admin = 0, Normal = 1 }

    public enum Semester : byte { Winter = 0, Summer = 1 }

    public enum Lang : byte
    {
        Polish = 0,
        English = 1,
        Deutch = 2
    }

    public enum WeekType : byte
    {
        All = 0,
        Even = 1,
        Odd = 2
    }

    public enum CourseType : byte
    {
        Other = 0,
        Lecture = 1,
        Lab = 2,
        Exercise = 3,
        Project = 4,
        Seminar = 5
    }
}