namespace UniversityWebApp.Models;

public partial class Student
{
    public Guid StudentId { get; set; }
    public Guid GroupId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public virtual Group Group { get; set; } = null!;
    public override string? ToString()
    {
        if (Group == null || Group.ToString() == "")
        {
            return $"{FirstName} {LastName}";
        }
        return $"{FirstName} {LastName} ({Group})";
    }
}
