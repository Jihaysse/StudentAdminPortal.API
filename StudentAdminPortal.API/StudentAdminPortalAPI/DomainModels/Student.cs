namespace StudentAdminPortalAPI.DomainModels
{
    public class Student
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; }
        public long Mobile { get; set; }
        public string? ProfileImageURL { get; set; }
        public Guid GenderID { get; set; }
        public Gender? Gender { get; set; }
        public Address? Address { get; set; }
    }
}
