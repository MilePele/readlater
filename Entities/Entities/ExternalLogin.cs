using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLater.Entities
{
    public class ExternalLogin : EntityBase
    {
        [Key]
        public int ID { get; set; }
        
        [StringLength(maximumLength: 256)]
        public string Token { get; set; }

        public DateTime TimeCreated { get; set; }

        public bool Expired { get; set; }
    }
}
