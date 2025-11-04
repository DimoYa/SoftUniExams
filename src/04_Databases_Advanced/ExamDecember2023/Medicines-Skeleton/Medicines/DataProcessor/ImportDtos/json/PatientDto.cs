using Medicines.Data.Models;
using Medicines.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicines.DataProcessor.ImportDtos.json
{
    public class PatientDto
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string FullName { get; set; }

        [Required]
        public string AgeGroup { get; set; }


        [Required]
        public string Gender { get; set; }

        [Required]
        public List<int> Medicines { get; set; } = new();
    }
}
