namespace Business.DTOs;

public class ProjectRegistrationForm
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string StartDate { get; set; } = null!;  //To Do: Detta är tillfällig lösning
    public string EndDate { get; set; } = null!;    //To Do: Detta är tillfällig lösning
    //public DateTime StartDate { get; set; }
    //public DateTime EndDate { get; set; }
    public int CustomerId { get; set; }     //Get int from drop down list on form (UI)
    public int StatusId { get; set; }       //Get int from drop down list on form (UI)
    public int UserId { get; set; }         //Get int from drop down list on form (UI)
    public int ServiceId { get; set; }      //Get int from drop down list on form (UI)
    //public int Total { get; set; }
    public decimal Total { get; set; }      //Will test!!!
}

