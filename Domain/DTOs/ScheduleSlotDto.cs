public class ScheduleSlotInsertDto
{
    public int DoctorId{get;set;}
    public int RoomId{get;set;}
    public DateTime? StartTime{get;set;}=DateTime.Now;
    public DateTime? EndTime{get;set;}=DateTime.Now;
    public bool IsActive{get;set;}=true;
}
public class ScheduleSlotUpdateDto
{
    public int Id{get;set;}
    public int DoctorId{get;set;}
    public int RoomId{get;set;}
    public DateTime? StartTime{get;set;}=DateTime.Now;
    public DateTime? EndTime{get;set;}=DateTime.Now;
    public bool IsActive{get;set;}=true;
}
public class ScheduleSlotDtoForJoinDoctors
{
    public int DoctorId{get;set;}
    public int RoomId{get;set;}
    public string? DoctorFullName{get;set;}
    public string? DoctorSpecialty{get;set;}
    public DateTime? StartTime{get;set;}=DateTime.Now;
    public DateTime? EndTime{get;set;}=DateTime.Now;
}