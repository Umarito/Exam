public class DoctorInsertDto
{
    public string? FullName{get;set;}
    public string? Specialty{get;set;}
    public bool IsActive{get;set;}
    public DateTime HiredAt{get;set;}=DateTime.Now;
}
public class DoctorUpdateDto
{
    public int Id{get;set;}
    public string? FullName{get;set;}
    public string? Specialty{get;set;}
    public bool IsActive{get;set;}
    public DateTime HiredAt{get;set;}=DateTime.Now;
}