using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastre.DataProcessor.ImportDtos.xml
{
    public class PharmacyDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^\(\d{3}\)\s\d{3}-\d{4}$")]
        public string PhoneNumber { get; set; }

        [Required]
        public string IsNonStop { get; set; }

        public List<MedicineDTo> Medicines { get; set; }
    }
}
