using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController(IAppointmentService AppointmentService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<string>> AddAsync(AppointmentInsertDto Appointment)
    {
        return await AppointmentService.AddAsync(Appointment);
    }
    [HttpGet]
    public async Task<List<Appointment>> GetAppointmentsAsync()
    {
        return await AppointmentService.GetAllAppointments();
    }
    
    [HttpGet("{AppointmentId}")]
    public async Task<Response<Appointment>> GetAppointmentByIdAsync(int AppointmentId)
    {
        return await AppointmentService.GetAppointmentById(AppointmentId);
    }
    [HttpPut("{AppointmentId}/{status}")]
    public async Task<Response<string>> ChangeStatusAsync(int AppointmentId,string status)
    {
        return await AppointmentService.ChangeStatus(AppointmentId,status);
    }
    [HttpDelete("{AppointmentId}")]
    public async Task<Response<string>> DeleteAsync(int AppointmentId)
    {
        return await AppointmentService.DeleteAsync(AppointmentId);
    }
}