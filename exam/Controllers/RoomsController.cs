using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RoomsController(IRoomService RoomService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<string>> AddAsync(RoomInsertDto Room)
    {
        return await RoomService.AddAsync(Room);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateAsync(RoomUpdateDto Room)
    {
        return await RoomService.UpdateAsync(Room);
    }
    [HttpGet]
    public async Task<List<Room>> GetRoomsAsync()
    {
        return await RoomService.GetAllRooms();
    }
    
    [HttpGet("{RoomId}")]
    public async Task<Response<Room>> GetRoomByIdAsync(int RoomId)
    {
        return await RoomService.GetRoomById(RoomId);
    }
    [HttpPatch("{RoomId}")]
    public async Task<Response<string>> SoftDeleteAsync(int RoomId)
    {
        return await RoomService.SoftDelete(RoomId);
    }
}