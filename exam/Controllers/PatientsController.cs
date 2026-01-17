using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PatientsController(IPatientService PatientService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<string>> AddAsync(PatientInsertDto Patient)
    {
        return await PatientService.AddAsync(Patient);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateAsync(PatientUpdateDto Patient)
    {
        return await PatientService.UpdateAsync(Patient);
    }
    [HttpGet]
    public async Task<List<Patient>> GetPatientsAsync()
    {
        return await PatientService.GetAllPatients();
    }
    
    [HttpGet("{PatientId}")]
    public async Task<Response<Patient>> GetPatientByIdAsync(int PatientId)
    {
        return await PatientService.GetPatientById(PatientId);
    }
    [HttpPatch("{PatientId}")]
    public async Task<Response<string>> SoftDeleteAsync(int PatientId)
    {
        return await PatientService.SoftDelete(PatientId);
    }
}