public class AppointmentInsertDto
{
    public int PatientId{get;set;}
    public int SlotId{get;set;}
    public string? Status{get;set;}
    public DateTime UpdatedAt{get;set;}
}
public class AppointmentUpdateDto
{
    public int Id{get;set;}
    public int PatientId{get;set;}
    public int SlotId{get;set;}
    public string? Status{get;set;}
    public DateTime UpdatedAt{get;set;}
}