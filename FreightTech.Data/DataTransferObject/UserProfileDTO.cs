namespace FreightTech.Data.DataTransferObject
{
    public class UserProfileDTO : RowObject
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public int RoleId { get; set; }
        public bool HasImage { get; set; }
    }
}
