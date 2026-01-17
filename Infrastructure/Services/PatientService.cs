using Microsoft.Extensions.Logging;
using Dapper;
using Npgsql;
using System.Net;

public class PatientService(ApplicationDBContext applicationDBContext, ILogger<PatientService> logger) : IPatientService
{
    private readonly ApplicationDBContext _context = applicationDBContext;
    private readonly ILogger<PatientService> _logger = logger;
    public async Task<Response<string>> AddAsync(PatientInsertDto patientInsertDto)
    {
        var Patient = new Patient()
        {
            FullName = patientInsertDto.FullName,
            BirthDate = patientInsertDto.BirthDate,
            Phone = patientInsertDto.Phone
        };
        try
        {
            var conn = _context.Connection();
            var query = "insert into patients(fullname,birth_date,phone) values(@name,@birth,@phone)";
            var res = await conn.ExecuteAsync(query,new{name=Patient.FullName,birth=Patient.BirthDate,phone=Patient.Phone});
            if(res == 0)
            {
                _logger.LogWarning("In the process something went wrong");
                return new Response<string>(HttpStatusCode.InternalServerError, "Patient not added");
            }
            else
            {
                _logger.LogInformation("In the process nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "Patient was added successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<List<Patient>> GetAllPatients()
    {
        var conn = _context.Connection();
        var query = "select * from patients";
        var res = await conn.QueryAsync<Patient>(query);
        return res.ToList();
    }

    public async Task<Response<Patient>> GetPatientById(int patientId)
    {
        try
        {
            var conn = _context.Connection();
            var query = "select * from patients where id = @id";
            var res = await conn.QueryFirstOrDefaultAsync<Patient>(query,new{id = patientId});
            if (res == null)
            {
                _logger.LogWarning("Something went wrong");
                return new Response<Patient>(HttpStatusCode.InternalServerError,"Couldn't find Patient");
            }
            else
            {
                _logger.LogInformation("Nothing went wrong");
                return new Response<Patient>(HttpStatusCode.OK,"The data that you were searching for:",res);
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<Patient>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> SoftDelete(int patientId)
    {
        try
        {
            var conn = _context.Connection();
            var query = "update patients set is_active=false where id = @id";
            var res = await conn.ExecuteAsync(query,new{id = patientId});
            if(res == 0)
            {
                _logger.LogWarning("Something went wrong in the process of deleting Patient");
                return new Response<string>(HttpStatusCode.InternalServerError, "Patient not deleted");
            }
            else
            {
                _logger.LogInformation("In the process of deleting Patient nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "Patient was deleted successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> UpdateAsync(PatientUpdateDto patientUpdateDto)
    {
        var Patient = new Patient()
        {
            FullName = patientUpdateDto.FullName,
            BirthDate = patientUpdateDto.BirthDate,
            Phone = patientUpdateDto.Phone,
            Id = patientUpdateDto.Id
        };
        try
        {
            var conn = _context.Connection();
            var query = "update patients set fullname=@name,birth_date=@birth,phone=@phone where id = @id";
            var res = await conn.ExecuteAsync(query,new{name=Patient.FullName,birth=Patient.BirthDate,phone=Patient.Phone,id=Patient.Id});
            if(res == 0)
            {   
                _logger.LogWarning("Something went wrong in the process of updating Patient");
                return new Response<string>(HttpStatusCode.InternalServerError, "Patient not updated");
            }
            else
            {
                _logger.LogInformation("In the process of updating Patient nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "Patient was updated successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
}