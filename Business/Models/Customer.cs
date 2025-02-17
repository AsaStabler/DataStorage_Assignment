namespace Business.Models;

public class Customer
{
    //NOTE! Det är en lista av Customers som ska visas i drop-down på Projectsidan
    //Behövs då Email resp. Phone mappas in i modellen Customer? Om de nu ändå inte ska visas?

    public int Id { get; set; }
    public string CustomerName { get; set; } = null!;
    public string CustomerEmail { get; set; } = null!; // ???
    public string CustomerPhone { get; set; } = null!; // ???
}
