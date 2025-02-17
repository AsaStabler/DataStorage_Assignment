namespace Business.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    //This did not help in the display of the DateTime,
    //both date and time were still displayed
    //DateTime måste antagligen parsas eller konvertas på något sätt.
    //men det kan också lösas med WPF date hantering (?!)
    //[Column(TypeName = "date")]
    public DateTime StartDate  { get; set; } 
    public DateTime EndDate { get; set; }
    
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;

    public int StatusId { get; set; }
    public string StatusName { get; set; } = null!;

    public int UserId { get; set; }
    public string UserDisplayName { get; set; } = null!;

    public int ServiceId { get; set; }
    public string ServiceDescription { get; set; } = null!;

    public decimal Total { get; set; }
}
