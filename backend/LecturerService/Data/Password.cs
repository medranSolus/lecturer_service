namespace LecturerService.Data
{
    public class Password
    {
        public string ID { get; set; }
        public string Hash { get; set; }

        public Password(Model.Password pass)
        {
            ID = pass.ID;
            Hash = pass.Hash;
        }
    }
}