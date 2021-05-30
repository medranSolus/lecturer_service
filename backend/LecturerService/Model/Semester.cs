using System.ComponentModel.DataAnnotations;

namespace LecturerService.Model
{
    public class Semester
    {
        [Key]
        public string ID { get; set; }

        [Required(ErrorMessage = "Must specify semester type!")]
        public bool IsWinter { get; set; }

        [Required(ErrorMessage = "Must specify semester start year!")]
        public uint StartYear { get; set; }

        [Required(ErrorMessage = "Starting month of semester cannot be empty!")]
        public byte StartMonth { get; set; }

        [Required(ErrorMessage = "Starting day of semester cannot be empty!")]
        public byte StartDay { get; set; }

        [Required(ErrorMessage = "Must specify semester end year!")]
        public uint EndYear { get; set; }

        [Required(ErrorMessage = "Ending month of semester cannot be empty!")]
        public byte EndMonth { get; set; }

        [Required(ErrorMessage = "Ending day of semester cannot be empty!")]
        public byte EndDay { get; set; }

        public void Update(Semester semester)
        {
            IsWinter = semester.IsWinter;
            StartYear = semester.StartYear;
            StartMonth = semester.StartMonth;
            StartDay = semester.StartDay;
            EndYear = semester.EndYear;
            EndMonth = semester.EndMonth;
            EndDay = semester.EndDay;
        }
    }
}