namespace Business.DTOs;

public class ProjectRegistrationForm
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }   
    public DateTime? EndDate { get; set; }    
    public int CustomerId { get; set; }     
    public int StatusId { get; set; }       
    public int UserId { get; set; }         
    public int ServiceId { get; set; }      
}

//Om man inte sätter "nullable", dvs DateTime?, så initieras DateTime till 0001-01-01
//och då visas detta datum istället för "Select a date" i DatePicker i WPF

//Man kan bara sätta DateTime? (inte null!) för DateTime. Då initieras den till null.

