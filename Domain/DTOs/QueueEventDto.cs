public class QueueEventInsertDto
{
    public int AppointmentId{get;set;}
    public string? EventType{get;set;}
}
public class QueueEventUpdateDto
{
    public int Id{get;set;}
    public int AppointmentId{get;set;}
    public string? EventType{get;set;}
}