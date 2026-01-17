public interface IPatientService
{
    Task<Response<string>> AddAsync(PatientInsertDto patientInsPatientInsertDto);
    Task<Response<Patient>> GetPatientById(int patientId);
    Task<List<Patient>> GetAllPatients();
    Task<Response<string>> UpdateAsync(PatientUpdateDto patientUpPatientUpdateDto);
    Task<Response<string>> SoftDelete(int patientId);
}