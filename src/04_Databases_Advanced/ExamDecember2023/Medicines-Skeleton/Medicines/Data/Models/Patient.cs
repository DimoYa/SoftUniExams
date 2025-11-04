using Medicines.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicines.Data.Models
{
    public class Patient
    {
        public Patient()
        {
            this.PatientsMedicines = new HashSet<PatientMedicine>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string FullName  { get; set; }

        [Required]
        public AgeGroup AgeGroup { get; set; }


        [Required]
        public Gender Gender { get; set; }

        [Required]
        public ICollection<PatientMedicine> PatientsMedicines { get; set; }
    }
}
