public interface IAppointmentService
{
    Task<Response<string>> AddAsync(AppointmentInsertDto appointmentInsertDto);
    Task<List<Appointment>> GetAllAppointments();
    Task<Response<Appointment>> GetAppointmentById(int appointmentId);
    Task<Response<Appointment>> GetDoctorFromAppointments(int doctorId);
    Task<Response<string>> ChangeStatus(int appointmentId,string status);
    Task<Response<string>> DeleteAsync(int appointmentId);
}