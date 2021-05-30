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

        [ForeignKey("Department")]
        [Required(ErrorMessage = "Course department cannot be empty!")]
        public Data.Department DepartmentID { get; set; }
        public EnumTab.Department Department { get; set; }

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

        [ForeignKey("Semester")]
        [Required(ErrorMessage = "Must specify semester of course!")]
        public string SemesterID { get; set; }
        public Semester Semester { get; set; }
        
        [Required(ErrorMessage = "Must specify year of course!")]
        public uint Year { get; set; }
#endregion // Basic info

#nullable enable
        [ForeignKey("Lecturer")]
        public string? LecturerID { get; set; }
        public Lecturer? Lecturer { get; set; }

        public string? CourseGroup { get; set; }
#nullable disable

        public Course() {}
        public Course(Data.Course course)
        {
            Update(course);
        }

        public void Update(Data.Course course)
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
            SemesterID = course.SemesterID;
            Year = course.Year;
            LecturerID = course.LecturerID;
            CourseGroup = course.CourseGroup;
        }
    }
}