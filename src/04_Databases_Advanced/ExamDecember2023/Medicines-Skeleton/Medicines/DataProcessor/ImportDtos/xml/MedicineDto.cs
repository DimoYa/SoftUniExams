using Medicines.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastre.DataProcessor.ImportDtos.xml
{
    public class MedicineDTo
    {
        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(0.01, 1000.00)]
        public string Price { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ProductionDate { get; set; }

        [Required]
        public string ExpiryDate { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Producer { get; set; }
    }
}
