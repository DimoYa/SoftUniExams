using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastre.Data.Models
{
    public class PropertyCitizen
    {
        [Key]
        [Required]
        public int PropertyId { get; set; }
        [ForeignKey("PropertyId")]
        public virtual Property Property { get; set; }

        [Key]
        [Required]
        public int CitizenId { get; set; }
        [ForeignKey("CitizenId")]
        public virtual Citizen Citizen { get; set; }
    }
}
