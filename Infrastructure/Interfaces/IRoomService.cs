public interface IRoomService
{
    Task<Response<string>> AddAsync(RoomInsertDto RoomInsertDto);
    Task<Response<Room>> GetRoomById(int RoomId);
    Task<List<Room>> GetAllRooms();
    Task<Response<string>> UpdateAsync(RoomUpdateDto RoomUpdateDto);
    Task<Response<string>> SoftDelete(int RoomId);
}