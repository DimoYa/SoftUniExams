using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastre.DataProcessor.ImportDtos.xml
{
    public class DistrictDto
    {
        [Required]
        public string Region { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^[A-Z]{2}-[0-9]{5}$")]
        public string PostalCode { get; set; }
        public List<PropertyDto> Properties { get; set; }
    }
}
