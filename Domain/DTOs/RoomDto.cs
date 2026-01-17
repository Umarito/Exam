public class RoomInsertDto
{
    public string? Name{get;set;}
    public bool IsActive{get;set;}=true;
}
public class RoomUpdateDto
{
    public int Id{get;set;}
    public string? Name{get;set;}
    public bool IsActive{get;set;}=true;
}