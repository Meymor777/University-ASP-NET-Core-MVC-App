namespace UniversityWebApp.Models;

public partial class Course
{
    public Guid CourseId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
    public override string? ToString()
    {
        return Name;
    }
}
