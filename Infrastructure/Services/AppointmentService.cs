using Microsoft.Extensions.Logging;
using Dapper;
using Npgsql;
using System.Net;

public class AppointmentService(ApplicationDBContext applicationDBContext, ILogger<AppointmentService> logger) : IAppointmentService
{
    private readonly ApplicationDBContext _context = applicationDBContext;
    private readonly ILogger<AppointmentService> _logger = logger;
    public async Task<Response<string>> AddAsync(AppointmentInsertDto appointmentInsertDto)
    {
        var Appointment = new Appointment()
        {
            PatientId = appointmentInsertDto.PatientId,
            SlotId = appointmentInsertDto.SlotId,
            Status = appointmentInsertDto.Status,
            UpdatedAt = appointmentInsertDto.UpdatedAt
        };
        try
        {
            var conn = _context.Connection();
            var query = "insert into appointments(patient_id,slot_id,status,updated_at) values(@pId,@sId,@status,@updated)";
            var res = await conn.ExecuteAsync(query,new{pId=Appointment.PatientId,sId=Appointment.SlotId,status=Appointment.Status,updated=Appointment.UpdatedAt});
            if(res == 0)
            {
                _logger.LogWarning("In the process something went wrong");
                return new Response<string>(HttpStatusCode.InternalServerError, "Appointment not added");
            }
            else
            {
                _logger.LogInformation("In the process nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "Appointment was added successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> ChangeStatus(int appointmentId, string status)
    {
        try
        {
            var conn = _context.Connection();
            var query = "update appointments set status =@Status where id = @id";
            var res = await conn.ExecuteAsync(query,new{id = appointmentId,Status=status});
            if(res == 0)
            {
                _logger.LogWarning("Something went wrong");
                return new Response<string>(HttpStatusCode.InternalServerError, "Appointment not deleted");
            }
            else
            {
                _logger.LogInformation("Nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "Appointment was deleted successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> DeleteAsync(int appointmentId)
    {
        try
        {
            var conn = _context.Connection();
            var query = "delete from appointments where id = @id";
            var res = await conn.ExecuteAsync(query,new{id = appointmentId});
            if(res == 0)
            {
                _logger.LogWarning("Something went wrong in the process of deleting Appointment");
                return new Response<string>(HttpStatusCode.InternalServerError, "Appointment not deleted");
            }
            else
            {
                _logger.LogInformation("In the process of deleting Appointment nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "Appointment was deleted successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<List<Appointment>> GetAllAppointments()
    {
        var conn = _context.Connection();
        var query = "select * from appointments";
        var res = await conn.QueryAsync<Appointment>(query);
        return res.ToList();
    }

    public async Task<Response<Appointment>> GetAppointmentById(int appointmentId)
    {
        try
        {
            var conn = _context.Connection();
            var query = "select * from appointments where id = @id";
            var res = await conn.QueryFirstOrDefaultAsync<Appointment>(query,new{id = appointmentId});
            if (res == null)
            {
                _logger.LogWarning("Something went wrong");
                return new Response<Appointment>(HttpStatusCode.InternalServerError,"Couldn't find Appointment");
            }
            else
            {
                _logger.LogInformation("Nothing went wrong");
                return new Response<Appointment>(HttpStatusCode.OK,"The data that you were searching for:",res);
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<Appointment>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<Appointment>> GetDoctorFromAppointments(int AppointmentId)
    {
        throw new NotImplementedException();
    }
}