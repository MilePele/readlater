using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLater.Entities
{
    public class Bookmark : EntityBase  
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Bookmark URL")]
        [StringLength(maximumLength: 500)]
        public string URL {get;set;}

        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        [Display(Name = "Category")]
        public virtual Category Category { get; set; }

        [Display(Name = "Date Created")]
        public DateTime CreateDate { get; set; }
    }
}
