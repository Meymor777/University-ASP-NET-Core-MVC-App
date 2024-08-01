namespace UniversityWebApp.Models;

public partial class Group
{
    public Guid GroupId { get; set; }
    public Guid CourseId { get; set; }
    public string Name { get; set; } = null!;
    public virtual Course Course { get; set; } = null!;
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    public override string? ToString()
    {
        if (Course == null || Course.ToString() == "")
        {
            return $"{Name}";
        }
        return $"{Name} ({Course})";
    }
}
