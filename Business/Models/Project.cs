namespace Business.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    public DateTime StartDate  { get; set; } 
    public DateTime? EndDate { get; set; }

    public string DisplayStartDate { get; set; } = null!;
    public string DisplayEndDate { get; set; } = null!;

    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;

    public int StatusId { get; set; }
    public string StatusName { get; set; } = null!;

    public int UserId { get; set; }
    public string UserDisplayName { get; set; } = null!;

    public int ServiceId { get; set; }
    public string ServiceDescription { get; set; } = null!;
}
