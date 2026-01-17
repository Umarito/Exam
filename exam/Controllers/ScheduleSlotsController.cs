using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ScheduleSlotsController(IScheduleSlotService ScheduleSlotService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<string>> AddAsync(ScheduleSlotInsertDto ScheduleSlot)
    {
        return await ScheduleSlotService.AddAsync(ScheduleSlot);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateAsync(ScheduleSlotUpdateDto ScheduleSlot)
    {
        return await ScheduleSlotService.UpdateAsync(ScheduleSlot);
    }
    [HttpGet]
    public async Task<List<ScheduleSlot>> GetScheduleSlotsAsync()
    {
        return await ScheduleSlotService.GetAllSlots();
    }
    
    [HttpGet("{ScheduleSlotId}")]
    public async Task<Response<ScheduleSlot>> GetScheduleSlotByIdAsync(int ScheduleSlotId)
    {
        return await ScheduleSlotService.GetSlotById(ScheduleSlotId);
    }
    [HttpPatch("{ScheduleSlotId}")]
    public async Task<Response<string>> SoftDeleteAsync(int ScheduleSlotId)
    {
        return await ScheduleSlotService.SoftDelete(ScheduleSlotId);
    }
}