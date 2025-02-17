namespace Business.DTOs;

public class ProjectUpdateForm
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    //public string? StartDate { get; set; }
    //public string? EndDate { get; set; }
    public DateTime? StartDate { get; set; }    //To Do: Test this
    public DateTime? EndDate { get; set; }      //To Do: Test this
    public int CustomerId { get; set; }    //int?  
    public int StatusId { get; set; }      //int? 
    public int UserId { get; set; }        //int? 
    public int ServiceId { get; set; }     //int? 

    // TO DO: vore bättre att sätta string här och sedan göra conversion i ProjectService istället för i MenuDialog
    public int? Total { get; set; }         //int? Är det korrekt att ha ? på en int???
    //public decimal? Total { get; set; }
}
