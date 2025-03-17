namespace kavitha.DTOS
{
  public class UserTypeDTO
  {
    public string Name { get; set; }
    public string Description { get; set; }

    public int Id { get; set; }  // ✅ Ensure the ID is included
  }
}
