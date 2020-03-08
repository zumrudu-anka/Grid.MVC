using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Grid.MVC.Models
{
    [Table("Personal")]
    public class Personal
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(25), Required]
        public string Name { get; set; }
        [StringLength(25), Required]
        public string Surname { get; set; }
        [Required]
        public int Age { get; set; }

        public Address Address { get; set; }
    }
}