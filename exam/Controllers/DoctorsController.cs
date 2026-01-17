using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController(IDoctorService DoctorService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<string>> AddAsync(DoctorInsertDto Doctor)
    {
        return await DoctorService.AddAsync(Doctor);
    }
    [HttpGet]
    public async Task<List<Doctor>> GetDoctorsAsync()
    {
        return await DoctorService.GetAllDoctors();
    }
    
    [HttpGet("{DoctorId}")]
    public async Task<Response<Doctor>> GetDoctorByIdAsync(int DoctorId)
    {
        return await DoctorService.GetDoctorById(DoctorId);
    }
    [HttpPatch("{DoctorId}")]
    public async Task<Response<string>> SoftDeleteAsync(int DoctorId)
    {
        return await DoctorService.SoftDelete(DoctorId);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateAsync(DoctorUpdateDto doctorUpdateDto)
    {
        return await DoctorService.UpdateAsync(doctorUpdateDto);
    }
}