namespace LecturerService.Data
{
    public class Password
    {
        public string ID { get; set; }
        public string Pass { get; set; }

        public Password() {}
        public Password(Model.Password pass)
        {
            ID = pass.ID;
            Pass = pass.Pass;
        }
    }
}