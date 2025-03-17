namespace kavitha.DTOS
{
  public class TravelRequestDTO
  {
    public int EmployeeId { get; set; }
    public string Destination { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Purpose { get; set; }
    public string Status { get; set; }
  }
}
