using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFCoreUI.Models
{
    public class Phone
    {
        public int Id { get; set; }

        [Required]
        public int ContactId { get; set; }

        [MaxLength(20)]
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string PhoneNumber { get; set; }
    }
}
