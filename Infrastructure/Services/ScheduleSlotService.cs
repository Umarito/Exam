using Microsoft.Extensions.Logging;
using Dapper;
using System.Net;
public class ScheduleSlotService(ApplicationDBContext applicationDBContext, ILogger<ScheduleSlotService> logger) : IScheduleSlotService
{
    private readonly ApplicationDBContext _context = applicationDBContext;
    private readonly ILogger<ScheduleSlotService> _logger = logger;
    public async Task<Response<string>> AddAsync(ScheduleSlotInsertDto ScheduleSlotInsertDto)
    {
        var ScheduleSlot = new ScheduleSlot()
        {
            DoctorId = ScheduleSlotInsertDto.DoctorId,
            RoomId = ScheduleSlotInsertDto.RoomId,
            StartTime = ScheduleSlotInsertDto.StartTime,
            EndTime = ScheduleSlotInsertDto.EndTime
        };
        try
        {
            var conn = _context.Connection();
            var query = "insert into schedule_slots(doctor_id,room_id,start_time,end_time) values(@dId,@rId,@startTime,@endTime)";
            var res = await conn.ExecuteAsync(query,new{dId=ScheduleSlot.DoctorId,rId=ScheduleSlot.RoomId,startTime=ScheduleSlot.StartTime,endTime=ScheduleSlot.EndTime});
            if(res == 0)
            {
                _logger.LogWarning("In the process something went wrong");
                return new Response<string>(HttpStatusCode.InternalServerError, "ScheduleSlot not added");
            }
            else
            {
                _logger.LogInformation("In the process nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "ScheduleSlot was added successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<List<ScheduleSlot>> GetAllSlots()
    {
        var conn = _context.Connection();
        var query = "select * from schedule_slots";
        var res = await conn.QueryAsync<ScheduleSlot>(query);
        return res.ToList();
    }

    public async Task<Response<ScheduleSlot>> GetSlotById(int ScheduleSlotId)
    {
        try
        {
            var conn = _context.Connection();
            var query = "select * from schedule_slots where id = @id";
            var res = await conn.QueryFirstOrDefaultAsync<ScheduleSlot>(query,new{id = ScheduleSlotId});
            if (res == null)
            {
                _logger.LogWarning("Something went wrong");
                return new Response<ScheduleSlot>(HttpStatusCode.InternalServerError,"Couldn't find ScheduleSlot");
            }
            else
            {
                _logger.LogInformation("Nothing went wrong");
                return new Response<ScheduleSlot>(HttpStatusCode.OK,"The data that you were searching for:",res);
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<ScheduleSlot>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> SoftDelete(int ScheduleSlotId)
    {
        try
        {
            var conn = _context.Connection();
            var query = "update schedule_Slots set is_active=false where id = @id";
            var res = await conn.ExecuteAsync(query,new{id = ScheduleSlotId});
            if(res == 0)
            {
                _logger.LogWarning("Something went wrong in the process of deleting ScheduleSlot");
                return new Response<string>(HttpStatusCode.InternalServerError, "ScheduleSlot not deleted");
            }
            else
            {
                _logger.LogInformation("In the process of deleting ScheduleSlot nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "ScheduleSlot was deleted successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> UpdateAsync(ScheduleSlotUpdateDto ScheduleSlotUpdateDto)
    {
        var ScheduleSlot = new ScheduleSlot()
        {
            DoctorId = ScheduleSlotUpdateDto.DoctorId,
            RoomId = ScheduleSlotUpdateDto.RoomId,
            StartTime = ScheduleSlotUpdateDto.StartTime,
            EndTime = ScheduleSlotUpdateDto.EndTime,
            Id = ScheduleSlotUpdateDto.Id
        };
        try
        {
            var conn = _context.Connection();
            var query = "update schedule_slots set doctor_id=@dId,room_id=@rId,start_time=@startTime,end_time=@endTime where id = @id";
            var res = await conn.ExecuteAsync(query,new{ndId=ScheduleSlot.DoctorId,rId=ScheduleSlot.RoomId,startTime=ScheduleSlot.StartTime,endTime=ScheduleSlot.EndTime,id=ScheduleSlot.Id});
            if(res == 0)
            {   
                _logger.LogWarning("Something went wrong in the process of updating ScheduleSlot");
                return new Response<string>(HttpStatusCode.InternalServerError, "ScheduleSlot not updated");
            }
            else
            {
                _logger.LogInformation("In the process of updating ScheduleSlot nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "ScheduleSlot was updated successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
    public Task<Response<ScheduleSlot>> GetSlotByDoctorId(int doctorId)
    {
        throw new NotImplementedException();
    }
}