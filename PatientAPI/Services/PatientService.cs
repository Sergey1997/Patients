using Microsoft.EntityFrameworkCore;
using PatientAPI.Data;
using PatientAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientAPI.Services
{
    public class PatientService : IPatientService
    {
        private readonly AppDbContext _context;

        public PatientService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Patient> CreatePatient(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<Patient?> GetPatientById(Guid id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task<IEnumerable<Patient>> SearchPatients(string birthDate)
        {
            if (string.IsNullOrEmpty(birthDate))
                return new List<Patient>();

            string[] validPrefixes = { "eq", "ne", "lt", "gt", "ge", "le", "sa", "eb", "ap" };
            string prefix = validPrefixes.FirstOrDefault(p => birthDate.StartsWith(p)) ?? "eq";
            string dateString = prefix == "eq" ? birthDate : birthDate.Substring(prefix.Length);

            if (!DateTime.TryParse(dateString, out DateTime searchDate))
                return new List<Patient>();

            IQueryable<Patient> query = _context.Patients;

            switch (prefix)
            {
                case "eq": query = query.Where(p => p.BirthDate.Date == searchDate.Date); break;
                case "ne": query = query.Where(p => p.BirthDate.Date != searchDate.Date); break;
                case "lt": query = query.Where(p => p.BirthDate.Date < searchDate.Date); break;
                case "gt": query = query.Where(p => p.BirthDate.Date > searchDate.Date); break;
                case "ge": query = query.Where(p => p.BirthDate.Date >= searchDate.Date); break;
                case "le": query = query.Where(p => p.BirthDate.Date <= searchDate.Date); break;
                case "sa": query = query.Where(p => p.BirthDate.Date > searchDate.Date); break;
                case "eb": query = query.Where(p => p.BirthDate.Date < searchDate.Date); break;
                case "ap":
                    query = query.Where(p => p.BirthDate.Date >= searchDate.AddDays(-1).Date &&
                                             p.BirthDate.Date <= searchDate.AddDays(1).Date);
                    break;
            }

            return await query.ToListAsync();
        }

        public async Task<bool> UpdatePatient(Guid id, Patient updatedPatient)
        {
            if (id != updatedPatient.Id)
                return false;

            _context.Entry(updatedPatient).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePatient(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
                return false;

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
