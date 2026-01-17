using Microsoft.Extensions.Logging;
using Dapper;
using Npgsql;
using System.Net;

public class DoctorService(ApplicationDBContext applicationDBContext, ILogger<DoctorService> logger) : IDoctorService
{
    private readonly ApplicationDBContext _context = applicationDBContext;
    private readonly ILogger<DoctorService> _logger = logger;
    public async Task<Response<string>> AddAsync(DoctorInsertDto doctorInsertDto)
    {
        var Doctor = new Doctor()
        {
            FullName = doctorInsertDto.FullName,
            Specialty = doctorInsertDto.Specialty,
            IsActive = doctorInsertDto.IsActive,
            HiredAt = doctorInsertDto.HiredAt
        };
        try
        {
            var conn = _context.Connection();
            var query = "insert into doctors(fullname,specialty,hired_at) values(@name,@spec,@hiredAt)";
            var res = await conn.ExecuteAsync(query,new{name=Doctor.FullName,spec=Doctor.Specialty,hiredAt=Doctor.HiredAt});
            if(res == 0)
            {
                _logger.LogWarning("In the process something went wrong");
                return new Response<string>(HttpStatusCode.InternalServerError, "Doctor not added");
            }
            else
            {
                _logger.LogInformation("In the process nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "Doctor was added successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<List<Doctor>> GetAllDoctors()
    {
        var conn = _context.Connection();
        var query = "select * from doctors";
        var res = await conn.QueryAsync<Doctor>(query);
        return res.ToList();
    }

    public async Task<Response<Doctor>> GetDoctorById(int DoctorId)
    {
        try
        {
            var conn = _context.Connection();
            var query = "select * from doctors where id = @id";
            var res = await conn.QueryFirstOrDefaultAsync<Doctor>(query,new{id = DoctorId});
            if (res == null)
            {
                _logger.LogWarning("Something went wrong");
                return new Response<Doctor>(HttpStatusCode.InternalServerError,"Couldn't find Doctor");
            }
            else
            {
                _logger.LogInformation("Nothing went wrong");
                return new Response<Doctor>(HttpStatusCode.OK,"The data that you were searching for:",res);
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<Doctor>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> SoftDelete(int DoctorId)
    {
        try
        {
            var conn = _context.Connection();
            var query = "update doctors set is_active=false where id = @id";
            var res = await conn.ExecuteAsync(query,new{id = DoctorId});
            if(res == 0)
            {
                _logger.LogWarning("Something went wrong in the process of deleting Doctor");
                return new Response<string>(HttpStatusCode.InternalServerError, "Doctor not deleted");
            }
            else
            {
                _logger.LogInformation("In the process of deleting Doctor nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "Doctor was deleted successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> UpdateAsync(DoctorUpdateDto doctorUpdateDto)
    {
        var Doctor = new Doctor()
        {
            FullName = doctorUpdateDto.FullName,
            Specialty = doctorUpdateDto.Specialty,
            IsActive = doctorUpdateDto.IsActive,
            HiredAt = doctorUpdateDto.HiredAt,
            Id = doctorUpdateDto.Id
        };
        try
        {
            var conn = _context.Connection();
            var query = "update doctors set fullname=@name,specialty=@spec,hired_at=@hiredAt where id = @id";
            var res = await conn.ExecuteAsync(query,new{name=Doctor.FullName,spec=Doctor.Specialty,hiredAt=Doctor.HiredAt,id=Doctor.Id});
            if(res == 0)
            {   
                _logger.LogWarning("Something went wrong in the process of updating Doctor");
                return new Response<string>(HttpStatusCode.InternalServerError, "Doctor not updated");
            }
            else
            {
                _logger.LogInformation("In the process of updating Doctor nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "Doctor was updated successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
}