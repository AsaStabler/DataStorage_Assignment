namespace Business.Models;

public class Customer
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = null!;
    public string CustomerEmail { get; set; } = null!; 
    public string CustomerPhone { get; set; } = null!; 
}
