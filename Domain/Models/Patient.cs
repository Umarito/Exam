public class Patient : BaseEntity
{
    public string? FullName{get;set;}
    public string? Phone{get;set;}
    public DateTime BirthDate{get;set;}
    public bool IsActive{get;set;}=true;
}