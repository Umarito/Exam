using Microsoft.Extensions.Logging;
using Dapper;
using System.Net;
public class QueueEventService(ApplicationDBContext applicationDBContext, ILogger<QueueEventService> logger) : IQueueEventService
{
    private readonly ApplicationDBContext _context = applicationDBContext;
    private readonly ILogger<QueueEventService> _logger = logger;
    public async Task<Response<string>> AddAsync(QueueEventInsertDto queueEventInsertDto)
    {
        var QueueEvent = new QueueEvent()
        {
            AppointmentId = queueEventInsertDto.AppointmentId,
            EventType = queueEventInsertDto.EventType
        };
        try
        {
            var conn = _context.Connection();
            var query = "insert into queue_events(appointment_id,event_type) values(@aId,@eventType)";
            var res = await conn.ExecuteAsync(query,new{aId=QueueEvent.AppointmentId,eventType=QueueEvent.EventType});
            if(res == 0)
            {
                _logger.LogWarning("In the process something went wrong");
                return new Response<string>(HttpStatusCode.InternalServerError, "QueueEvent not added");
            }
            else
            {
                _logger.LogInformation("In the process nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "QueueEvent was added successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        try
        {
            var conn = _context.Connection();
            var query = "delete from queue_events where id = @id";
            var res = await conn.ExecuteAsync(query,new{id = id});
            if(res == 0)
            {
                _logger.LogWarning("Something went wrong in the process of deleting QueueEvent");
                return new Response<string>(HttpStatusCode.InternalServerError, "QueueEvent not deleted");
            }
            else
            {
                _logger.LogInformation("In the process of deleting QueueEvent nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "QueueEvent was deleted successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<List<QueueEvent>> GetAllQueueEvents()
    {
        var conn = _context.Connection();
        var query = "select * from queue_events";
        var res = await conn.QueryAsync<QueueEvent>(query);
        return res.ToList();
    }

    public async Task<Response<QueueEvent>> GetQueueEventById(int QueueEventId)
    {
        try
        {
            var conn = _context.Connection();
            var query = "select * from queue_events where id = @id";
            var res = await conn.QueryFirstOrDefaultAsync<QueueEvent>(query,new{id = QueueEventId});
            if (res == null)
            {
                _logger.LogWarning("Something went wrong");
                return new Response<QueueEvent>(HttpStatusCode.InternalServerError,"Couldn't find QueueEvent");
            }
            else
            {
                _logger.LogInformation("Nothing went wrong");
                return new Response<QueueEvent>(HttpStatusCode.OK,"The data that you were searching for:",res);
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<QueueEvent>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> UpdateAsync(QueueEventUpdateDto queueEventUpdateDto)
    {
        var QueueEvent = new QueueEvent()
        {
            AppointmentId = queueEventUpdateDto.AppointmentId,
            EventType = queueEventUpdateDto.EventType,
            Id = queueEventUpdateDto.Id
        };
        try
        {
            var conn = _context.Connection();
            var query = "update queue_events set appointment_id=@aId,event_type=@type where id = @id";
            var res = await conn.ExecuteAsync(query,new{aId=QueueEvent.AppointmentId,type=QueueEvent.EventType,id=QueueEvent.Id});
            if(res == 0)
            {   
                _logger.LogWarning("Something went wrong in the process of updating QueueEvent");
                return new Response<string>(HttpStatusCode.InternalServerError, "QueueEvent not updated");
            }
            else
            {
                _logger.LogInformation("In the process of updating QueueEvent nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "QueueEvent was updated successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
}