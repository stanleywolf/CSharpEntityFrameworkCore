using System;
using System.Collections.Generic;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    public class Diagnose
    {
        public Diagnose()
        {
            this.Patients = new List<Patient>();
        }

        public int DiagnoseId { get; set; }

        public string Name { get; set; }

        public string Comments { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public ICollection<Patient> Patients { get; set; }


    }
}
