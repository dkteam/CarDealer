﻿using CarDealer.Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealer.Model.Models
{
    [Table("Periods")]
    public class Period : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(20)]
        public string Name { set; get; }

        [Required]
        [MaxLength(30)]
        [Column(TypeName = "varchar")]
        public string Alias { set; get; }

        [MaxLength(256)]
        public string Description { set; get; }

        public virtual IEnumerable<InstallmentPeriod> InstallmentPeriods { set; get; }
    }
}