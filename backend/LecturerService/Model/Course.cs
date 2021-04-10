using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LecturerService.Model
{
    public class Course
    {
        [Key]
        public string ID { get; set; }

        public bool Accepted { get; set; }

#region Basic info
        [Required(ErrorMessage = "Course name cannot be empty!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Course department cannot be empty!")]
        public string Department { get; set; }

        [ForeignKey("CourseType")]
        [Required(ErrorMessage = "Course type cannot be empty!")]
        public Data.CourseType TypeID { get; set; }
        public EnumTab.CourseType CourseType { get; set; }

        [ForeignKey("Lang")]
        [Required(ErrorMessage = "Must specify course language!")]
        public Data.Lang LanguageTypeID { get; set; }
        public EnumTab.Lang Lang { get; set; }

        [Required(ErrorMessage = "Must specify course ECTS!")]
        public byte Ects { get; set; }

        [Required(ErrorMessage = "Must specify course hours at university!")]
        public byte HoursUniversity { get; set; }

        [Required(ErrorMessage = "Must specify course individual student work hours!")]
        public byte HoursStudent { get; set; }
#endregion // Basic info

#region Dates
        [ForeignKey("Semester")]
        [Required(ErrorMessage = "Must specify semester of course!")]
        public Data.Semester SemesterTypeID { get; set; }
        public EnumTab.Semester Semester { get; set; }
        
        [Required(ErrorMessage = "Must specify year of course!")]
        public uint Year { get; set; }

        [Required(ErrorMessage = "Starting month of course cannot be empty!")]
        public byte StartMonth { get; set; }

        [Required(ErrorMessage = "Starting day of course cannot be empty!")]
        public byte StartDay { get; set; }

        [Required(ErrorMessage = "Ending month of course cannot be empty!")]
        public byte EndMonth { get; set; }

        [Required(ErrorMessage = "Ending day of course cannot be empty!")]
        public byte EndDay { get; set; }
#endregion // Dates

#nullable enable
        [ForeignKey("Lecturer")]
        public string? LecturerID { get; set; }
        public Lecturer? Lecturer { get; set; }

        public string? CourseGroup { get; set; }
#nullable disable

        public Course() {}
        public Course(Data.Course course)
        {
            ID = course.ID;
            Accepted = course.Accepted;
            Name = course.Name;
            Department = course.Department;
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