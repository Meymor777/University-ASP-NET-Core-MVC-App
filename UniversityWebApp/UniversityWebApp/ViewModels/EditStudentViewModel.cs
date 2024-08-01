namespace UniversityWebApp.ViewModels
{
    public class EditStudentViewModel
    {
        public Guid GroupId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
