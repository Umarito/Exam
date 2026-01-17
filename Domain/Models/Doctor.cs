public class Doctor : BaseEntity
{
    public string? FullName{get;set;}
    public string? Specialty{get;set;}
    public bool IsActive{get;set;}
    public DateTime HiredAt{get;set;}=DateTime.Now;
}