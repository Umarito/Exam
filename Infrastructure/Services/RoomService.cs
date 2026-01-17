using Microsoft.Extensions.Logging;
using Dapper;
using System.Net;
public class RoomService(ApplicationDBContext applicationDBContext, ILogger<RoomService> logger) : IRoomService
{
    private readonly ApplicationDBContext _context = applicationDBContext;
    private readonly ILogger<RoomService> _logger = logger;
    public async Task<Response<string>> AddAsync(RoomInsertDto RoomInsertDto)
    {
        var Room = new Room()
        {
            Name = RoomInsertDto.Name
        };
        try
        {
            var conn = _context.Connection();
            var query = "insert into rooms(name) values(@name)";
            var res = await conn.ExecuteAsync(query,new{name=Room.Name});
            if(res == 0)
            {
                _logger.LogWarning("In the process something went wrong");
                return new Response<string>(HttpStatusCode.InternalServerError, "Room not added");
            }
            else
            {
                _logger.LogInformation("In the process nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "Room was added successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<List<Room>> GetAllRooms()
    {
        var conn = _context.Connection();
        var query = "select * from rooms";
        var res = await conn.QueryAsync<Room>(query);
        return res.ToList();
    }

    public async Task<Response<Room>> GetRoomById(int RoomId)
    {
        try
        {
            var conn = _context.Connection();
            var query = "select * from rooms where id = @id";
            var res = await conn.QueryFirstOrDefaultAsync<Room>(query,new{id = RoomId});
            if (res == null)
            {
                _logger.LogWarning("Something went wrong");
                return new Response<Room>(HttpStatusCode.InternalServerError,"Couldn't find Room");
            }
            else
            {
                _logger.LogInformation("Nothing went wrong");
                return new Response<Room>(HttpStatusCode.OK,"The data that you were searching for:",res);
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<Room>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> SoftDelete(int RoomId)
    {
        try
        {
            var conn = _context.Connection();
            var query = "update rooms set is_active=false where id = @id";
            var res = await conn.ExecuteAsync(query,new{id = RoomId});
            if(res == 0)
            {
                _logger.LogWarning("Something went wrong in the process of deleting Room");
                return new Response<string>(HttpStatusCode.InternalServerError, "Room not deleted");
            }
            else
            {
                _logger.LogInformation("In the process of deleting Room nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "Room was deleted successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> UpdateAsync(RoomUpdateDto RoomUpdateDto)
    {
        var Room = new Room()
        {
            Name = RoomUpdateDto.Name,
            Id = RoomUpdateDto.Id
        };
        try
        {
            var conn = _context.Connection();
            var query = "update rooms set name=@name where id = @id";
            var res = await conn.ExecuteAsync(query,new{name=Room.Name,id=Room.Id});
            if(res == 0)
            {   
                _logger.LogWarning("Something went wrong in the process of updating Room");
                return new Response<string>(HttpStatusCode.InternalServerError, "Room not updated");
            }
            else
            {
                _logger.LogInformation("In the process of updating Room nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "Room was updated successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
}