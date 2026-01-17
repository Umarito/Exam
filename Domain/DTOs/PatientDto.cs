public class PatientInsertDto
{
    public string? FullName{get;set;}
    public string? Phone{get;set;}
    public DateTime BirthDate{get;set;}
    public bool IsActive{get;set;}=true;
}
public class PatientUpdateDto
{
    public int Id{get;set;}
    public string? FullName{get;set;}
    public string? Phone{get;set;}
    public DateTime BirthDate{get;set;}
    public bool IsActive{get;set;}=true;
}