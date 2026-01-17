public class Appointment : BaseEntity
{
    public int PatientId{get;set;}
    public int SlotId{get;set;}
    public string? Status{get;set;}
    public DateTime UpdatedAt{get;set;}
}