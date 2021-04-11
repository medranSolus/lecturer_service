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

    public enum Day : byte
    {
        Monday = 0,
        Tuesday = 1,
        Wednesday = 2,
        Thursday = 3,
        Friday = 4
    }

    public enum WeekType : byte
    {
        All = 0,
        Even = 1,
        Odd = 2
    }

    public enum Department : byte
    {
        W1 = 0,
        W2 = 1,
        W3 = 2,
        W4 = 3,
        W5 = 4,
        W6 = 5,
        W7 = 6,
        W8 = 7,
        W9 = 8,
        W10 = 9,
        W11 = 10,
        W12 = 11,
        W13 = 12,
        FJG = 13,
        FW = 14,
        FL = 15
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