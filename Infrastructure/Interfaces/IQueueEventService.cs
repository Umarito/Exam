public interface IQueueEventService
{
    Task<Response<string>> AddAsync(QueueEventInsertDto QueueEventInsertDto);
    Task<Response<QueueEvent>> GetQueueEventById(int QueueEventId);
    Task<List<QueueEvent>> GetAllQueueEvents();
    Task<Response<string>> UpdateAsync(QueueEventUpdateDto QueueEventUpdateDto);
    Task<Response<string>> DeleteAsync(int id);
}