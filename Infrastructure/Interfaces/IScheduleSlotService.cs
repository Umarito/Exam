public interface IScheduleSlotService
{
    Task<Response<string>> AddAsync(ScheduleSlotInsertDto scheduleSlotInsScheduleSlotInsertDto);
    Task<List<ScheduleSlot>> GetAllSlots();
    Task<Response<ScheduleSlotDtoForJoinDoctors>> GetSlotByDoctorId(int doctorId);
    Task<Response<string>> UpdateAsync(ScheduleSlotUpdateDto scheduleSlotUpdateDto);
    Task<Response<string>> SoftDelete(int slotId);
}