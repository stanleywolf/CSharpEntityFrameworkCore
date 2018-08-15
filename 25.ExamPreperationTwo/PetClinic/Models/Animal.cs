using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetClinic.Models
{
    public class Animal
    {
        //-	Id – integer, Primary Key
        //-	Name – text with min length 3 and max length 20 (required)
        //-	Type – text with min length 3 and max length 20 (required)
        //-	Age – integer, cannot be negative or 0 (required)
        //-	PassportSerialNumber ¬– string, foreign key
        //-	Passport – the passport of the animal(required)
        //-	Procedures – the procedures, performed on the animal

        public int Id { get; set; }

        [Required]
        [StringLength(20,MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Type { get; set; }

        [Required]
        [Range(1,Int32.MaxValue)]
        public int Age { get; set; }

        [ForeignKey(nameof(Passport))]
        public string PassportSerialNumber { get; set; }
        [Required]
        public Passport Passport { get; set; }

        public ICollection<Procedure> Procedures { get; set; } = new HashSet<Procedure>();

    }
}
