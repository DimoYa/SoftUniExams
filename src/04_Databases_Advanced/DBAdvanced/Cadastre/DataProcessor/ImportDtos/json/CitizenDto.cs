using Cadastre.DataProcessor.ImportDtos.xml;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastre.DataProcessor.ImportDtos.json
{
    public class CitizenDto
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        public string MaritalStatus { get; set; }

        [Required]
        public string BirthDate { get; set; }

        [JsonProperty("Properties")]
        public List<int> Properties { get; set; }
    }
}
