using Cadastre.Data.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastre.Data.Models
{
    public class District
    {
        public District()
        {
            this.Properties = new HashSet<Property>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(80, MinimumLength =2)]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^[A-Z]{2}-[0-9]{5}$")]
        public string PostalCode { get; set; }

        [Required]
        public Region Region { get; set; }

        public ICollection<Property> Properties { get; set; }
    }
}
