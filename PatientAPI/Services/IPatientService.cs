using PatientAPI.Models;

namespace PatientAPI.Services
{
    public interface IPatientService
    {
        Task<Patient> CreatePatient(Patient patient);
        Task<Patient?> GetPatientById(Guid id);
        Task<IEnumerable<Patient>> SearchPatients(string birthDate);
        Task<bool> UpdatePatient(Guid id, Patient updatedPatient);
        Task<bool> DeletePatient(Guid id);
    }
}
