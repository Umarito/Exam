public interface IScheduleSlotService
{
    Task<Response<string>> AddAsync(ScheduleSlotInsertDto scheduleSlotInsScheduleSlotInsertDto);
    Task<List<ScheduleSlot>> GetAllSlots();
    Task<Response<ScheduleSlot>> GetSlotById(int slotId);
    Task<Response<ScheduleSlot>> GetSlotByDoctorId(int doctorId);
    Task<Response<string>> UpdateAsync(ScheduleSlotUpdateDto scheduleSlotUpdateDto);
    Task<Response<string>> SoftDelete(int slotId);
}