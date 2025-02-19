namespace Business.DTOs;

public class ProjectUpdateForm
{
    public string? Title { get; set; }          //Behöver Title vara nullable (?)
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }    //To Do: Test this
    public DateTime? EndDate { get; set; }      //To Do: Test this
    public int CustomerId { get; set; }      
    public int StatusId { get; set; }       
    public int UserId { get; set; }        
    public int ServiceId { get; set; }     

    // TO DO: vore bättre att sätta string här och sedan göra conversion i ProjectService istället för i MenuDialog
    public int? Total { get; set; }        
    //public decimal? Total { get; set; }
}
