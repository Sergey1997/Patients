using Microsoft.AspNetCore.Mvc;
using PatientAPI.Models;
using PatientAPI.Services;

namespace PatientAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        public async Task<ActionResult<Patient>> CreatePatient(Patient patient)
        {
            var createdPatient = await _patientService.CreatePatient(patient);
            return CreatedAtAction(nameof(GetPatientById), new { id = createdPatient.Id }, createdPatient);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatientById(Guid id)
        {
            var patient = await _patientService.GetPatientById(id);
            return patient == null ? NotFound() : Ok(patient);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Patient>>> SearchPatients([FromQuery] string birthDate)
        {
            var patients = await _patientService.SearchPatients(birthDate);
            return patients == null || !patients.Any() ? NotFound("No patients found.") : Ok(patients);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(Guid id, Patient updatedPatient)
        {
            var success = await _patientService.UpdatePatient(id, updatedPatient);
            return success ? NoContent() : BadRequest("Update failed.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(Guid id)
        {
            var success = await _patientService.DeletePatient(id);
            return success ? NoContent() : NotFound("Patient not found.");
        }
    }
}
