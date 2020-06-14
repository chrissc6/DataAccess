using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFCoreUI.Models
{
    public class Email
    {
        public int Id { get; set; }

        [Required]
        public int ContactId { get; set; }

        [MaxLength(100)]
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string EmailAddress { get; set; }
    }
}
