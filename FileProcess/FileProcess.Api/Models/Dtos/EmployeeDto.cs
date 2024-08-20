namespace FileProcess.Api.Models.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Department { get; set; }
        public DateTime LastUpdateAt { get; set; }
    }
}
