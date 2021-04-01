namespace LecturerService.Data
{
    public class GroupMsgInfo
    {
        public long ID { get; set; }
        public Group Group { get; set; }

        public GroupMsgInfo(Model.GroupMsg msg)
        {
            ID = msg.ID;
            Group = new Group(msg.Group);
            Group.LecturerID = msg.LecturerID;
        }
    }
}