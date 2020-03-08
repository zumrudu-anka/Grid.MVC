using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Grid.MVC.Models
{
    [Table("Country")]
    public class Country
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50),Required]
        public string Name { get; set; }

        public List<Address> Addresses { get; set; }
    }
}