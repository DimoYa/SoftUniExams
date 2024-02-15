using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastre.DataProcessor.ImportDtos.xml
{
    public class PropertyDto
    {
        [Required]
        [StringLength(20, MinimumLength = 16)]
        public string PropertyIdentifier { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Area { get; set; }

        [StringLength(500, MinimumLength = 5)]
        public string Details { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 5)]
        public string Address { get; set; }

        [Required]
        public string DateOfAcquisition { get; set; }
    }
}
