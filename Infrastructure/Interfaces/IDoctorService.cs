public interface IDoctorService
{
    Task<Response<string>> AddAsync(DoctorInsertDto DoctorInsertDto);
    Task<Response<Doctor>> GetDoctorById(int DoctorId);
    Task<List<Doctor>> GetAllDoctors();
    Task<Response<string>> UpdateAsync(DoctorUpdateDto DoctorUpdateDto);
    Task<Response<string>> SoftDelete(int DoctorId);
}