using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class QueueEventsController(IQueueEventService QueueEventService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<string>> AddAsync(QueueEventInsertDto QueueEvent)
    {
        return await QueueEventService.AddAsync(QueueEvent);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateAsync(QueueEventUpdateDto QueueEvent)
    {
        return await QueueEventService.UpdateAsync(QueueEvent);
    }
    [HttpGet]
    public async Task<List<QueueEvent>> GetQueueEventsAsync()
    {
        return await QueueEventService.GetAllQueueEvents();
    }
    
    [HttpGet("{QueueEventId}")]
    public async Task<Response<QueueEvent>> GetQueueEventByIdAsync(int QueueEventId)
    {
        return await QueueEventService.GetQueueEventById(QueueEventId);
    }
    [HttpDelete("{QueueId}")]
    public async Task<Response<string>> DeleteAsync(int QueueId)
    {
        return await QueueEventService.DeleteAsync(QueueId);
    }
}