using MaritzaData.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaritzaData
{
    public class tblTopics : tblBase
    {
        [Key]
        [Required]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TopicID { get; set; }
        [Required]
        [Display(Name = "Temática")]
        public string Name { get; set; }
    }
}
