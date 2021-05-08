namespace LecturerService.Data
{
    public class Lecturer
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public Role RoleTypeID { get; set; }

#nullable enable
        public string? Phone { get; set; }
        public string? Title { get; set; }
#nullable disable

        public Lecturer() {}
        public Lecturer(Model.Lecturer lecturer)
        {
            ID = lecturer.ID;
            Name = lecturer.Name;
            Surname = lecturer.Surname;
            Mail = lecturer.Mail;
            RoleTypeID = lecturer.RoleTypeID;
            Phone = lecturer.Phone;
            Title = lecturer.Title;
        }
    }
}