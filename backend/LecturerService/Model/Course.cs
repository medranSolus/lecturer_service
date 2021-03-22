using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LecturerService.Model
{
    // Bits 7-0: Day, bits 15-8: Month
    using PackedDate = System.UInt16;

    public class Course
    {
        [Key]
        public string ID { get; set; }

#region Basic info
        [Required(ErrorMessage = "Course name cannot be empty!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Course type cannot be empty!")]
        public Data.CourseType Type { get; set; }

        [Required(ErrorMessage = "Must specify course language!")]
        public Data.Lang Language { get; set; }

        [Required(ErrorMessage = "Must specify course ECTS!")]
        public byte Ects { get; set; }

        [Required(ErrorMessage = "Must specify course hours at university!")]
        public byte HoursUniversity { get; set; }

        [Required(ErrorMessage = "Must specify course individual student work hours!")]
        public byte HoursStudent { get; set; }
#endregion // Basic info

#region Dates
        [Required(ErrorMessage = "Must specify semester of course!")]
        public Data.Semester Semester { get; set; }
        
        [Required(ErrorMessage = "Must specify year of course!")]
        public uint Year { get; set; }

        [Required(ErrorMessage = "Must specify week of course!")]
        public Data.WeekType WeekType { get; set; }

        [Required(ErrorMessage = "Start of course cannot be empty!")]
        public PackedDate Start { get; set; }

        [Required(ErrorMessage = "End of course cannot be empty!")]
        public PackedDate End { get; set; }
#endregion // Dates

#nullable enable
        [ForeignKey("Lecturer")]
        public string? LecturerID { get; set; }
        public Lecturer? Lecturer { get; set; }
        public string? CourseFlow { get; set; }
        public string? Group { get; set; }
#nullable disable
    }
}