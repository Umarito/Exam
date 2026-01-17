public class ScheduleSlot : BaseEntity
{
    public int DoctorId{get;set;}
    public int RoomId{get;set;}
    public DateTime? StartTime{get;set;}=DateTime.Now;
    public DateTime? EndTime{get;set;}=DateTime.Now;
    public bool IsActive{get;set;}=true;
}